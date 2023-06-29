import { HttpClient, HttpParams } from '@angular/common/http';
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
    return this.http.get<TipoProductoModel[]>(`${this.baseUrl}TipoProducto/ListaTipoProducto`);
  }

  listaTipoProductoFiltrado():Observable<TipoProductoModel[]>{
    return this.http.get<TipoProductoModel[]>(`${this.baseUrl}TipoProducto/ListaTipoProductoFiltrado`);
  }
  guardarTipoUsuario(TipoProducto:TipoProductoModel):Observable<TipoProductoModel>{
    return this.http.post<TipoProductoModel>(`${this.baseUrl}TipoProducto/GuardarTipoProducto`,TipoProducto);
  }
  obetenerTipoProductoPorId(id:number):Observable<TipoProductoModel>{
    const params = new HttpParams()
    .set('id',id)
    return this.http.get<TipoProductoModel>(`${this.baseUrl}TipoProducto/ObtenerTipoProductoPorId`,{ params });
  }

  sugerencias(dato:string):Observable<TipoProductoModel[]>{
    const params = new HttpParams()
    .set('dato',dato)
    return this.http.get<TipoProductoModel[]>(`${this.baseUrl}TipoProducto/BuscarTipoProductoPorDescripcion`,{ params });
  }
}
