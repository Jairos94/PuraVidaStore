import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BodegaModel } from '../models/bodega-model';

@Injectable({
  providedIn: 'root'
})
export class BodegaService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  listaUsuarios(): Observable<BodegaModel[]> {
    return this.http.get<BodegaModel[]>(`${this.baseUrl}Bodega/ListaBodegas`);
  }

  listaUsuariosPorDescripcion(descripcion:string): Observable<BodegaModel[]> {
    const params = new HttpParams()
    .set('Descripcion', descripcion);
    return this.http.get<BodegaModel[]>(`${this.baseUrl}Bodega/ListaBodegasPorDescripcion`,{params});
  }

  bodegaPorId(id:number): Observable<BodegaModel> {
    const params = new HttpParams()
    .set('id', id);
    return this.http.get<BodegaModel>(`${this.baseUrl}Bodega/BodegaPorId`,{params});
  }

  guardarBodega(bodega:BodegaModel): Observable<BodegaModel> {
    return this.http.post<BodegaModel>(`${this.baseUrl}Bodega/GuardarBodega`,bodega);
  }

}

