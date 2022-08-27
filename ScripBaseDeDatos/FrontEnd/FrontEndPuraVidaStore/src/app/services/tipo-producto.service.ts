import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TipoProductoModel } from '../models/tipo-producto';

@Injectable({
  providedIn: 'root'
})
export class TipoProductoService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient,) { }

  listaTipoProducto():Observable<TipoProductoModel[]>{
    return this.http.get<TipoProductoModel[]>(`${this.baseUrl}TipoProduco/ListaTipoProducto`);
  }
}
