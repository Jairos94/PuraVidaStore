import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { InventariosModel } from 'src/app/models/inventarios-model';
import { ProductoPorTrasladoModel } from 'src/app/models/producto-por-traslado-model';
import { TrasladoEntreBodegasModel } from 'src/app/models/traslado-entre-bodegas-model';
import { BodegaService } from 'src/app/services/bodega.service';
import { MovimientosService } from 'src/app/services/movimientos.service';

@Component({
  selector: 'app-traslados',
  templateUrl: './traslados.component.html',
  styleUrls: ['./traslados.component.css'],
  providers: [MessageService],
})
export class TrasladosComponent implements OnInit {
  esEditar: boolean = false;
  listaInventarios: InventariosModel[] = [];
  inventariosTabla: any[] = [];
  inventariosSeleccionadosNoFiltrado: any[] = [];
  listaBodegas: BodegaModel[] = [];
  habilitarModal: boolean = false;
  bodegaSeleccionada: BodegaModel = {
    bdgId: 0,
    bdgDescripcion: '',
    bdgVisible: true,
  };

  Traslado: TrasladoEntreBodegasModel = {
    idBodegaOrigen: 0,
    idBodegaDestino: 0,
    idUsuario: 0,
    productos: null,
  };
  buscador: string = '';
  inventarioSeleccionado: any = {
    producto: {
      prdId: 0,
      prdNombre: '',
      prdPrecioVentaMayorista: 0,
      prdPrecioVentaMinorista: 0,
      prdCodigo: '',
      prdUnidadesMinimas: 0,
      prdIdTipoProducto: 0,
      prdCodigoProvedor: '',
      pdrVisible: true,
      pdrFoto: '',
      pdrTieneExistencias: true,
      prdIdTipoProductoNavigation: null,
    },
    cantidadExistencia: 0,
    undsMaximas: 0,
  };

  constructor(
    private consultasMovimientos: MovimientosService,
    private bodegasServicio: BodegaService,
    private messageService: MessageService,
    private ruta: Router
  ) {}

  ngOnInit(): void {
    this.exisitencias();
    this.bodegas();
  }

  exisitencias() {
    this.consultasMovimientos
      .ProductosExistencias(activo.bodegaIngreso.bdgId)
      .subscribe({
        next: (x) => {
          this.listaInventarios = [];
          this.listaInventarios = x;
        },
        error: (_e) => console.log(_e),
      });
  }

  bodegas() {
    this.bodegasServicio.listaBodegas().subscribe({
      next: (x) => {
        x.forEach((bodega) => {
          if (bodega.bdgId != activo.bodegaIngreso.bdgId) {
            this.listaBodegas.push(bodega);
          }
        });
      },
      error: (_e) => console.log(_e),
    });
  }

  limpiarInventarioSeleccionado() {
    this.inventarioSeleccionado = {
      producto: {
        prdId: 0,
        prdNombre: '',
        prdPrecioVentaMayorista: 0,
        prdPrecioVentaMinorista: 0,
        prdCodigo: '',
        prdUnidadesMinimas: 0,
        prdIdTipoProducto: 0,
        prdCodigoProvedor: '',
        pdrVisible: true,
        pdrFoto: '',
        pdrTieneExistencias: true,
        prdIdTipoProductoNavigation: null,
      },
      cantidadExistencia: 0,
    };
  }

  activarModal(inventario: InventariosModel) {
    this.limpiarInventarioSeleccionado();
    this.inventarioSeleccionado.producto = inventario.producto;
    this.inventarioSeleccionado.undsMaximas = inventario.cantidadExistencia;

    this.habilitarModal = true;
  }

  buscarCodigoEnExistencias() {
    this.esEditar = false;
    let dato;
    let filtroBusqueda = this.listaInventarios;
    let datoAConsultar = filtroBusqueda.filter(
      (x) =>
        x.producto.prdCodigo === this.buscador ||
        x.producto.prdCodigoProvedor === this.buscador
    );
    if (datoAConsultar.length > 0) {
      dato = datoAConsultar[0];
      this.limpiarInventarioSeleccionado();

      this.inventarioSeleccionado.producto = dato.producto;
      this.inventarioSeleccionado.undsMaximas = dato.cantidadExistencia;
      this.inventarioSeleccionado.cantidadExistencia = 1;
      let seSumo = this.sumoAlInventario();
      if (seSumo) {
        this.enfoque();
      } else {
        this.mostrarError(
          'Cantidad con exceso',
          'La cantidad no debe sobre pasar el maximo'
        );

        this.enfoque();
      }
    } else {
      this.mostrarError(
        'Producto sin existencias',
        'El producto debe tener existencias'
      );

      this.enfoque();
    }
  }

  sumoAlInventario(): boolean {
    let retorno = false;
    if (!this.esEditar) {
      if (this.inventariosSeleccionadosNoFiltrado.length > 0) {
        this.inventariosSeleccionadosNoFiltrado.forEach((x) => {
          if (x.producto.prdId === this.inventarioSeleccionado.producto.prdId) {
            let totalASumar =
              x.cantidadExistencia +
              this.inventarioSeleccionado.cantidadExistencia;
            if (totalASumar <= x.undsMaximas) {
              x.cantidadExistencia =
                this.inventarioSeleccionado.cantidadExistencia +
                x.cantidadExistencia;
              retorno = true;
            } else {
              false;
            }
          } else {
            retorno = false;
          }
        });
      } else {
        this.inventariosSeleccionadosNoFiltrado.push(
          this.inventarioSeleccionado
        );
        retorno = true;
      }
      this.inventariosTabla = [];
      this.inventariosTabla = this.inventariosSeleccionadosNoFiltrado;
    } else {
      let lista = this.inventariosSeleccionadosNoFiltrado;
      let consulta = lista.filter((x) => {
        x.producto.prdId === this.inventarioSeleccionado.producto.prdId;
      });
      let dato = consulta[0];
      dato.cantidadExistencia = this.inventarioSeleccionado.cantidadExistencia;
      if (dato.cantidadExistencia <= dato.undsMaximas) {
        this.inventariosSeleccionadosNoFiltrado.forEach((x) => {
          if (x.producto.prdId === this.inventarioSeleccionado.producto.prdId) {
            x.cantidadExistencia = dato.cantidadExistencia;
          }
        });
      } else {
        this.mostrarError(
          'Cantidad con exceso',
          'La cantidad no debe sobre pasar el maximo'
        );
      }
      this.inventariosTabla = [];
      this.inventariosTabla = this.inventariosSeleccionadosNoFiltrado;
    }

    return retorno;
  }

  editarUnidades(inventariosEditar: any) {
    this.limpiarInventarioSeleccionado();
    this.inventarioSeleccionado = inventariosEditar;
    this.esEditar = true;
    this.habilitarModal = true;
  }

  enfoque() {
    this.buscador = '';
  }

  actualizarTraslado() {
    this.habilitarModal = false;
    let sumainvetario = this.sumoAlInventario();

    if (!sumainvetario) {
      this.mostrarError(
        'Cantidad con exceso',
        'La cantidad no debe sobre pasar el maximo'
      );
    }
  }

  removerLista(traslado: any) {
    const index = this.inventariosSeleccionadosNoFiltrado.indexOf(traslado, 0);
    if (index > -1) {
      this.inventariosSeleccionadosNoFiltrado.splice(index, 1);
    }
  }

  Guardar() {
    let productosGuardar: ProductoPorTrasladoModel[] = [];
    this.inventariosSeleccionadosNoFiltrado.forEach((x) => {
      let producto: ProductoPorTrasladoModel = {
        idProducto: x.producto.prdId,
        cantidad: x.cantidadExistencia,
      };
      productosGuardar.push(producto);
    });

    this.Traslado = {
      idBodegaOrigen: activo.bodegaIngreso.bdgId,
      idBodegaDestino: this.bodegaSeleccionada.bdgId,
      idUsuario: activo.usuarioPrograma.usrId,
      productos: productosGuardar,
    };
    this.consultasMovimientos.guardarTraslado(this.Traslado).subscribe({
      next: (x) => {
        this.ruta.navigate(['./principal/movimientos']);
      },
      error: (_e) => console.log(_e),
    });
  }

  mostrarError(encabezado: string, mensaje: string) {
    this.messageService.add({
      severity: 'error',
      summary: encabezado,
      detail: mensaje,
    });
  }
}
