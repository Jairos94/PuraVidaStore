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
  constructor() {}

  ngOnInit(): void {}



  leerArchivo(imagen: any): string {

    if (imagen == null) {
      imagen = '';
    }
    return Archivo.lectorImagen(imagen);
  }
}
