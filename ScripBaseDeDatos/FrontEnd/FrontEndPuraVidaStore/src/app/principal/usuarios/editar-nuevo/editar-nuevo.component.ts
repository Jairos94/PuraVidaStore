import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-editar-nuevo',
  templateUrl: './editar-nuevo.component.html',
  styleUrls: ['./editar-nuevo.component.css']
})
export class EditarNuevoComponent implements OnInit {
id:number=0;
  constructor() {
   }

  ngOnInit(): void {
  }

}
