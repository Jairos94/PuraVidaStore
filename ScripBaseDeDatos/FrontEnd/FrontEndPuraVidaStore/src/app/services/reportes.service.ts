import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ReporteMovimientoModel } from '../models/reporte-movimiento-model';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  obtenerDatosReportes(idBodega:number,fechaInico:Date,fechaFin:Date):Observable<ReporteMovimientoModel[]>
  {
    const params = new HttpParams()
    .set('id', idBodega)
    .set('FechaInicio', fechaInico.toDateString())
    .set('FechaFin', fechaInico.toDateString());
      return this.http.get<ReporteMovimientoModel[]>(`${this.baseUrl}Reportes/ReporteMovimeintos`);

  }
}
