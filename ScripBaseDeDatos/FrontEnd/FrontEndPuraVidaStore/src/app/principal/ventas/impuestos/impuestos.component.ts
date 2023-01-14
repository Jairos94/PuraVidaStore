import { Component, OnInit } from '@angular/core';
import { ImpuestosModel } from 'src/app/models/impuestos-model';

@Component({
  selector: 'app-impuestos',
  templateUrl: './impuestos.component.html',
  styleUrls: ['./impuestos.component.css'],
})
export class ImpuestosComponent implements OnInit {

  listaImpuestos: ImpuestosModel[] = [];

  constructor() {}

  ngOnInit(): void {}

  obtenerLista() {}
}
