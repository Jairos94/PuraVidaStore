import { Component, OnInit } from '@angular/core';
import { TipoProductoModel } from 'src/app/models/tipo-producto';

@Component({
  selector: 'app-agregar-editar',
  templateUrl: './agregar-editar.component.html',
  styleUrls: ['./agregar-editar.component.css']
})
export class AgregarEditarComponent implements OnInit {
  listaTipoProductos: TipoProductoModel[] = []
  private archivoTemporal: any;
  constructor() { }

  ngOnInit(): void {
  }

  archivo(evento: any) {
    const [file] = evento.currentFiles;
    console.log(file);
  }

}
