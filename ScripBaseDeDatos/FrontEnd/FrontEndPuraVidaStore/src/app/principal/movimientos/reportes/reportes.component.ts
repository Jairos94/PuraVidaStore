import { Component, OnInit } from '@angular/core';
import { activo } from 'src/app/activo';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent implements OnInit{
  fechaInico:Date|undefined;
  fechaFin:Date|undefined;

  constructor() {}

  ngOnInit(): void {

  }

  generarReporte(){
  }

}
