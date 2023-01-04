import { MovimientosService } from './../../../../services/movimientos.service';
import { Component, OnInit } from '@angular/core';
import { MotivoMovimientoModel } from 'src/app/models/motivo-movimiento-model';

@Component({
  selector: 'app-razon-ajustes',
  templateUrl: './razon-ajustes.component.html',
  styleUrls: ['./razon-ajustes.component.css'],
})
export class RazonAjustesComponent implements OnInit {
  constructor(private servicio: MovimientosService) {}

  listaMotivosMovimientos: MotivoMovimientoModel[] = [];

  ngOnInit(): void {
    this.servicio.obtenerMotivosMovimientos().subscribe({
      next: (x) => {
        this.listaMotivosMovimientos = [];
        this.listaMotivosMovimientos = x;
      },
      error: (_e) => {
        console.log(_e);
      },
    });
  }
}
