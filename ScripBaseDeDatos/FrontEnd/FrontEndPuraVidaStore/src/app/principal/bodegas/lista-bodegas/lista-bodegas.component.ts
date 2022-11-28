import { Component, OnInit } from '@angular/core';
import { BodegaModel } from 'src/app/models/bodega-model';
import { BodegaService } from 'src/app/services/bodega.service';

@Component({
  selector: 'app-lista-bodegas',
  templateUrl: './lista-bodegas.component.html',
  styleUrls: ['./lista-bodegas.component.css']
})
export class ListaBodegasComponent implements OnInit {

  listaBodegas:BodegaModel[]=[];

  constructor(private ServicioBodega: BodegaService) { }

  ngOnInit(): void {
    this.ObtenerBodegas();
  }


  ObtenerBodegas(){
    this.ServicioBodega.listaUsuarios().subscribe((x=>{
      this.listaBodegas=[];
      this.listaBodegas=x;
      
    }),(_e=>console.error(_e)));
  }
}
