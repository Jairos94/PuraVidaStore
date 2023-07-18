import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ReporteMovimientoModel } from '../models/reporte-movimiento-model';
import { FacturaReporteModel } from '../models/factura-reporte-model';

@Injectable({
  providedIn: 'root'
})
export class ReportesService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  obteneReporteMovimientos(idBodega:number,fechaInico:Date,fechaFin:Date):Observable<ReporteMovimientoModel[]>
  {
    const params = new HttpParams()
    .set('IdBodega', idBodega)
    .set('FechaInicio', fechaInico.toDateString())
    .set('FechaFin', fechaFin.toDateString()+' 23:59');
      return this.http.get<ReporteMovimientoModel[]>(`${this.baseUrl}Reportes/ReporteMovimeintos`,{params});

  }

  obteneReporteMovimientosProductos(idBodega:number,fechaInico:Date,fechaFin:Date,producto:string):Observable<ReporteMovimientoModel[]>
  {
    const params = new HttpParams()
    .set('IdBodega', idBodega)
    .set('FechaInicio', fechaInico.toDateString())
    .set('FechaFin', fechaFin.toDateString()+' 23:59')
    .set('Codigo', producto);
      return this.http.get<ReporteMovimientoModel[]>(`${this.baseUrl}Reportes/ReporteMovimientosPorProductos`,{params});

  }

  obteneReporteVentasPorBodega(idBodega:number,fechaInico:Date,fechaFin:Date):Observable<FacturaReporteModel>
  {
    const params = new HttpParams()
    .set('IdBodega', idBodega)
    .set('FechaInicio', fechaInico.toDateString())
    .set('FechaFin', fechaFin.toDateString()+' 23:59')
      return this.http.get<FacturaReporteModel>(`${this.baseUrl}Reportes/ReporteVentasBodega`,{params});

  }
}
