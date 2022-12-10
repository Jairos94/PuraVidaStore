import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InventariosModel } from '../models/inventarios-model';

@Injectable({
  providedIn: 'root'
})
export class MovimientosService {

  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  ProductosExistencias(idBodega:number):Observable<InventariosModel[]>
  {
    const params = new HttpParams()
      .set('IdBodega', idBodega);
    return this.http.get<InventariosModel[]>(`${this.baseUrl}Movimientos/Inventarios`, { params });
  }
}
