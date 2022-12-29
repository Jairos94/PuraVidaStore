import { ProductoModel } from 'src/app/models/producto-model';
import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { InventariosModel } from 'src/app/models/inventarios-model';
import { Component, OnInit } from '@angular/core';
import { Archivo } from 'src/app/utils/Archivos';
import { MovimientosService } from 'src/app/services/movimientos.service';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { timer } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ingresos',
  templateUrl: './ingresos.component.html',
  styleUrls: ['./ingresos.component.css'],
  providers: [MessageService],
})
export class IngresosComponent implements OnInit {
  listaIngresoInventarios: InventariosModel[] = [];
  codigo: string = '';

  producto: ProductoModel = {
    prdId: 0,
    prdNombre: '',
    prdPrecioVentaMayorista: 0,
    prdPrecioVentaMinorista: 0,
    prdCodigo: '',
    prdUnidadesMinimas: 0,
    prdIdTipoProducto: 0,
    prdCodigoProvedor: null,
    pdrVisible: true,
    pdrFoto: null,
    pdrTieneExistencias: true,
    prdIdTipoProductoNavigation: null,
  };

  productos: ProductoModel[] = [];

  constructor(
    private servicioProducto: ProductoServiceService,
    private servicioMovimiento: MovimientosService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig,
    private ruta: Router,
  ) {}

  ngOnInit(): void {
    this.primengConfig.ripple = true;
  }

  leerArchivo(imagen: any): string {
    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }

  buscarProductoPorCodigo() {
    this.servicioProducto.ObtenerProductoPorCodigo(this.codigo).subscribe({
      next: (x) => {
        var inventario: InventariosModel = {
          producto: x,
          cantidadExistencia: 1,
        };

        if (!this.validarIngreso(inventario)) {
          this.listaIngresoInventarios.push(inventario);
        }

        this.codigo = '';
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  buscarPorDescripcion(inventario: InventariosModel) {
    if (!this.validarIngreso(inventario)) {
      this.listaIngresoInventarios.push(inventario);
    }
  }

  validarIngreso(inventario: InventariosModel): boolean {
    var retorno = false;
    var hayAlgo = false;

    hayAlgo = this.listaIngresoInventarios.length > 0;
    if (hayAlgo) {
      this.listaIngresoInventarios.forEach((x) => {
        if (x.producto.prdId === inventario.producto.prdId) {
          x.cantidadExistencia =
            x.cantidadExistencia + inventario.cantidadExistencia;
          retorno = true;
        }
      });
    }

    return retorno;
  }

  guardar() {
    this.servicioMovimiento
      .GuardarSinOrden(this.listaIngresoInventarios)
      .subscribe({
        next: (x) => {
          this.addSingle();
          timer(5000).subscribe(x=>{
            this.ruta.navigate(['./principal/movimientos'])
          });

        },
        error: (_e) => {
          console.log(_e);
        },
      });
  }

  addSingle() {
    this.messageService.add({severity:'success', summary:'Service Message', detail:'Ingreso de productos correctamente'});
}
}
