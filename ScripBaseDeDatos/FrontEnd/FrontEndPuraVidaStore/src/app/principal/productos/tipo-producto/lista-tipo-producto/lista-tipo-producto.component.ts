import { Component, OnInit } from '@angular/core';
import { TipoProductoModel } from 'src/app/models/tipo-producto';


@Component({
  selector: 'app-lista-tipo-producto',
  templateUrl: './lista-tipo-producto.component.html',
  styleUrls: ['./lista-tipo-producto.component.css']
})
export class ListaTipoProductoComponent implements OnInit {
  checked: boolean=true;
  listaTipoProducto:TipoProductoModel[]=[]
  constructor() { }

  ngOnInit(): void {
  }

}
