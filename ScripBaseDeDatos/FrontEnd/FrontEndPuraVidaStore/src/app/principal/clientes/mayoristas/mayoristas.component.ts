import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { activo } from 'src/app/activo';
import { MayoristaModel } from 'src/app/models/mayorista-model';
import { MayoristaService } from 'src/app/services/mayorista.service';

@Component({
  selector: 'app-mayoristas',
  templateUrl: './mayoristas.component.html',
  styleUrls: ['./mayoristas.component.css'],
})
export class MayoristasComponent implements OnInit {
  listaClientes: MayoristaModel[] = [];
  buscador: string = '';
  esAdministrador: boolean = activo.esAministrador();

  constructor(private servicioMayorista: MayoristaService) {}

  ngOnInit(): void {
    this.ObtenerLista();
  }

  validarFecha(FechaVencimiento: string): string {
    let fechaActual = moment(new Date());
    let fecVencimiento = moment(FechaVencimiento);
    let dias;
    let meses;
    let years;
    let retorno = '';
    if (fecVencimiento >= fechaActual) {
      dias = fecVencimiento.diff(fechaActual, 'days');
      meses = fecVencimiento.diff(fechaActual, 'months');
      years = fecVencimiento.diff(fechaActual, 'years');
      retorno =
        'Tiempo para el vencimiento: ' +
        years +
        ' años, ' +
        meses +
        ' meses, ' +
        dias +
        ' dias.';
    } else {
      dias = fechaActual.diff(fecVencimiento, 'days');
      meses = fechaActual.diff(fecVencimiento, 'months');
      years = fechaActual.diff(fecVencimiento, 'years');
      retorno =
        'Tiempo vencido: ' +
        years +
        ' años, ' +
        meses +
        ' meses, ' +
        dias +
        ' dias.';
    }
    return retorno;
  }

  EstaVencido(FechaVencimiento: string): boolean {
    let fecVencimiento = moment(FechaVencimiento);
    let fechaActual = moment(new Date());

    if (fecVencimiento >= fechaActual) {
      return false;
    } else {
      return true;
    }
  }

  ObtenerLista() {
    this.servicioMayorista.obtenerListaMayorista().subscribe({
      next: (x) => {
        this.listaClientes = [];
        this.listaClientes = x;
      },
      error: (_e) => console.log(_e),
    });
  }

  filtrarTabla() {
    this.servicioMayorista.sugerencias(this.buscador).subscribe({
      next: (x) => {
        if (x.length > 0) {
          this.listaClientes = [];
          this.listaClientes = x;
        } else {
          this.ObtenerLista();
        }
      },
      error: (_e) => console.log(_e),
    });
  }
}
