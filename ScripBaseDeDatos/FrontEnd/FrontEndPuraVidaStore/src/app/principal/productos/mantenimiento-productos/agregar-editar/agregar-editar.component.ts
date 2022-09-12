import { Component, OnInit } from '@angular/core';
import { TipoProductoModel } from 'src/app/models/tipo-producto';
import { TipoProductoService } from 'src/app/services/tipo-producto.service';

@Component({
  selector: 'app-agregar-editar',
  templateUrl: './agregar-editar.component.html',
  styleUrls: ['./agregar-editar.component.css']
})
export class AgregarEditarComponent implements OnInit {

  listaTipoProductos: TipoProductoModel[] = []
  private archivoTemporal: any;
  constructor(private servicioTipoProducto:TipoProductoService) { }

  ngOnInit(): void {
    this.listaTipoProductoFiltrado();
  }

  archivo(evento: any) {
    const [file] = evento.currentFiles;
    console.log(file);
  }

  listaTipoProductoFiltrado(){
    this.servicioTipoProducto.listaTipoProductoFiltrado().subscribe((tp=>{
    this.listaTipoProductos = tp;
    console.log(this.listaTipoProductos);
    }),(_e=>console.log(_e)));
  }
}
