import { Component, OnInit } from '@angular/core';
import { activo } from 'src/app/activo';
import { BodegaModel } from 'src/app/models/bodega-model';
import { ReporteMovimientoModel } from 'src/app/models/reporte-movimiento-model';
import { BodegaService } from 'src/app/services/bodega.service';
import { ReportesService } from 'src/app/services/reportes.service';

@Component({
  selector: 'app-reporte-movimientos',
  templateUrl: './reporte-movimientos.component.html',
  styleUrls: ['./reporte-movimientos.component.css'],
})
export class ReporteMovimientosComponent implements OnInit {
  listaReporteMovimientos: ReporteMovimientoModel[] = [];
  listaBodegas: BodegaModel[] = [];

  bodegaSeleccionada: BodegaModel = activo.bodegaIngreso;
  idBodega: number = 0;
  fechaInicio: Date | undefined;
  fechaFin: Date | undefined;

  constructor(
    private reportesServicio: ReportesService,
    private bodegasServicio: BodegaService
  ) {}

  ngOnInit(): void {
    this.cargarBodegas();
  }

  cargarBodegas() {
    this.bodegasServicio.listaBodegas().subscribe(
      {
        next:x=>
        {
          this.listaBodegas=[];
          this.listaBodegas=x
        },
        error:_e=>console.log(_e)

      });
  }

  cargarPDFListadoMovimiento() {}
}
