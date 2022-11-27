import { Component, OnInit } from '@angular/core';
import { BodegaService } from 'src/app/services/bodega.service';

@Component({
  selector: 'app-lista-bodegas',
  templateUrl: './lista-bodegas.component.html',
  styleUrls: ['./lista-bodegas.component.css']
})
export class ListaBodegasComponent implements OnInit {

  constructor(private ServicioBodega: BodegaService) { }

  ngOnInit(): void {
    this.ObtenerBodegas();
  }


  ObtenerBodegas(){
    this.ServicioBodega.listaUsuarios().subscribe((x=>{
      console.log(x);
      
    }),(_e=>console.error(_e)));
  }
}
