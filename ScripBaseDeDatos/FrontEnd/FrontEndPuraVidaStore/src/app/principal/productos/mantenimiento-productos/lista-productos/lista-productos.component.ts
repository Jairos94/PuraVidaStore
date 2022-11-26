import { Component, OnInit } from '@angular/core';
import { ProductoServiceService } from 'src/app/services/producto-service.service';

@Component({
  selector: 'app-lista-productos',
  templateUrl: './lista-productos.component.html',
  styleUrls: ['./lista-productos.component.css']
})
export class ListaProductosComponent implements OnInit {

  constructor( private servicioProducto: ProductoServiceService) { }

  ngOnInit(): void {
  }

}
