import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ParametrosGlobalesModel } from '../models/parametros-globales-model';

@Injectable({
  providedIn: 'root',
})
export class ParametrosService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) {}

  ObtenerPametros(idBodega: number): Observable<ParametrosGlobalesModel> {
    const params = new HttpParams().set('idBodega', idBodega);

    return this.http.get<ParametrosGlobalesModel>(
      `${this.baseUrl}Parametros/ObtenerParametros`,
      { params }
    );
  }

  GuardarParametros(datos:ParametrosGlobalesModel):Observable<ParametrosGlobalesModel>
  {
    return this.http.post<ParametrosGlobalesModel>(
      `${this.baseUrl}Parametros/GuardarParametrosGlobales`,datos
    );
  }

}
