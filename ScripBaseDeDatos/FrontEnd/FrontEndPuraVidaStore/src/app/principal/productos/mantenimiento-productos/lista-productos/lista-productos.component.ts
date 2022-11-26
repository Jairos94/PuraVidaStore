import { Component, OnInit } from '@angular/core';
import { ProductoModel } from 'src/app/models/producto-model';
import { ProductoServiceService } from 'src/app/services/producto-service.service';
import { Archivo } from 'src/app/utils/Archivos';

@Component({
  selector: 'app-lista-productos',
  templateUrl: './lista-productos.component.html',
  styleUrls: ['./lista-productos.component.css']
})
export class ListaProductosComponent implements OnInit {

  constructor(private servicioProducto: ProductoServiceService) { }
  listaProductos: ProductoModel[] = [];

  ngOnInit(): void {
    this.CargarLista();
    
  }

  CargarLista() {
    this.servicioProducto.ListaProductoService().subscribe((x => {
      this.listaProductos= [];
      this.listaProductos = x;
      console.log(this.listaProductos);
      
    }), (_e => {console.log(_e);}));
  }


  leerArchivo(imagen:any):string{

    if(imagen == null)
    {
      imagen ='';
    }
   return Archivo.lectorImagen(imagen);
  }
}
