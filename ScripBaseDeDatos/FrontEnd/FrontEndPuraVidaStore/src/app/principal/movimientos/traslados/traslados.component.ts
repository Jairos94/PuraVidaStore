import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { InventariosModel } from 'src/app/models/inventarios-model';
import { BodegaService } from 'src/app/services/bodega.service';
import { MovimientosService } from 'src/app/services/movimientos.service';

@Component({
  selector: 'app-traslados',
  templateUrl: './traslados.component.html',
  styleUrls: ['./traslados.component.css'],
  providers: [MessageService],
})
export class TrasladosComponent implements OnInit {
  listaInventarios: InventariosModel[] = [];
  inventariosTabla: any[] = [];
  listaBodegas: BodegaModel[] = [];
  habilitarModal: boolean = false;
  bodegaSeleccionada: BodegaModel = {
    bdgId: 0,
    bdgDescripcion: '',
    bdgVisible: true,
  };
  productosSeleccionados: InventariosModel[] = [];

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
    undsMaximas:0
  };

  constructor(
    private consultasMovimientos: MovimientosService,
    private bodegasServicio: BodegaService,
    private messageService: MessageService
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
    this.inventarioSeleccionado.producto=inventario.producto;
    this.inventarioSeleccionado.undsMaximas=inventario.cantidadExistencia;
    this.habilitarModal= true;
  }
}
