import { Component, OnInit } from '@angular/core';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { BodegaService } from 'src/app/services/bodega.service';

@Component({
  selector: 'app-lista-bodegas',
  templateUrl: './lista-bodegas.component.html',
  styleUrls: ['./lista-bodegas.component.css']
})
export class ListaBodegasComponent implements OnInit {

  listaBodegas: BodegaModel[] = [];
  display: boolean = false;
  header: string = '';

  bodega: BodegaModel = {
    bdgId: 0,
    bdgDescripcion: '',
    bdgVisible: true
  }


  constructor(private ServicioBodega: BodegaService,) { }

  ngOnInit(): void {
    this.ObtenerBodegas();
  }


  ObtenerBodegas() {
    this.ServicioBodega.listaUsuarios().subscribe((x => {
      this.listaBodegas = [];
      this.listaBodegas = x;

    }), (_e => console.error(_e)));
  }


  AgregarBodega() {
    this.display = true;
    this.header = 'Ingresar Bodega';
  }

  EditarBodega(id: number) {

    this.display = true;
    this.header = 'Editar Bodega';

    this.ServicioBodega.bodegaPorId(id).subscribe((x => {
      this.bodega = x;


    }), (_e => console.error(_e)));
  }

  deshabilitarEliminarBodegaActual(id: number): boolean {
    if (id === activo.bodegaIngreso.bdgId) {
      return true;
    }
    else { return false }
  }

  GuardarBodega() {
    if (this.bodega.bdgId > 0 && this.bodega.bdgId == activo.bodegaIngreso.bdgId) {
      activo.bodegaIngreso = this.bodega;
    }
    this.ServicioBodega.guardarBodega(this.bodega).subscribe((x => {
 
    }), (_e => console.error(_e)));
  }


}
