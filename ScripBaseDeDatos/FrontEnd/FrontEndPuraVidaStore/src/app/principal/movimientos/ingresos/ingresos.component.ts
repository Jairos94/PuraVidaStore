import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { InventariosModel } from 'src/app/models/inventarios-model';
import { Component, OnInit } from '@angular/core';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-ingresos',
  templateUrl: './ingresos.component.html',
  styleUrls: ['./ingresos.component.css'],
})
export class IngresosComponent implements OnInit {
  listaIngresoInventarios: InventariosModel[] = [];
  codigo: string = '';
  constructor(private servicioProducto: ProductoServiceService) {}

  ngOnInit(): void {}

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

        if (! this.validarIngreso(inventario)) {
          this.listaIngresoInventarios.push(inventario);
        }

        this.codigo=''
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }

  validarIngreso(inventario: InventariosModel): boolean {
    var retorno = false;
    var hayAlgo = false;

    hayAlgo =this.listaIngresoInventarios.length > 0;
    if(hayAlgo){
      this.listaIngresoInventarios.forEach(x=>{
        if(x.producto.prdId === inventario.producto.prdId){

          x.cantidadExistencia = x.cantidadExistencia+ inventario.cantidadExistencia
          retorno = true

        }
      });
    }

    return retorno;
  }
}
