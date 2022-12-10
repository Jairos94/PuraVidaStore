import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { BodegaService } from 'src/app/services/bodega.service';

@Component({
  selector: 'app-lista-bodegas',
  templateUrl: './lista-bodegas.component.html',
  styleUrls: ['./lista-bodegas.component.css'],
  providers: [MessageService]
})
export class ListaBodegasComponent implements OnInit {

  listaBodegas: BodegaModel[] = [];
  display: boolean = false;
  header: string = '';
  buscar: string = '';

  bodega: BodegaModel = {
    bdgId: 0,
    bdgDescripcion: '',
    bdgVisible: true
  }


  constructor(private ServicioBodega: BodegaService, private messageService: MessageService) { }

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
    this.ServicioBodega.guardarBodega(this.bodega).subscribe((x => {
      this.ObtenerBodegas();
      this.display = false;
      this.mensajeExito();

    }), (_e => console.error(_e)));

    if (this.bodega.bdgId > 0 && this.bodega.bdgId == activo.bodegaIngreso.bdgId) {
      activo.bodegaIngreso = this.bodega;
    }


  }

  borrarBodega(bodegaParametro: BodegaModel) {
    bodegaParametro.bdgVisible = false;
    this.ServicioBodega.guardarBodega(bodegaParametro).subscribe((x => {

      this.mensajeError();
      this.ObtenerBodegas();

    }), (_e => console.error(_e)));

  }


  buscarPorDescription() {
    if(this.buscar!=''){

      this.ServicioBodega.listaUsuariosPorDescripcion(this.buscar).subscribe((x => {
        if(x.length>0 )
        {
          this.listaBodegas=[];
          this.listaBodegas=x;      
        }else{
          this.ObtenerBodegas()
        }
        }), (_e => console.error(_e)));
    }else{
      this.ObtenerBodegas()
    }

  }

  mensajeExito() {
    this.messageService.add({ severity: 'success', summary: 'Éxito al guardar', detail: 'Se guardó con éxito al guargar' });
  }

  mensajeError() {
    this.messageService.add({ severity: 'error', summary: 'Bodega de eliminada', detail: 'Se eliminó la bodega seleccionada' });

  }

}
