import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProductoModel } from '../models/producto-model';

@Injectable({
  providedIn: 'root'
})
export class ProductoServiceService {
  private baseUrl: string = environment.urlBase;
  constructor(private http: HttpClient) { }

  GuardarProducto(Producto:ProductoModel,idUsuario:number):Observable<ProductoModel>{
    //idUsuario
    const params = new HttpParams()
    .set('idUsuario', idUsuario);
    return this.http.post<ProductoModel>(`${this.baseUrl}Productos/GuardarProducto`,Producto,{params});
  }
  ListaProductoService():Observable<ProductoModel[]>{
    return this.http.get<ProductoModel[]>(`${this.baseUrl}Productos/ListaProductos`);
  }

  ProductoPorID(id:number):Observable<ProductoModel>{
    const params = new HttpParams()
    .set('id', id);
    return this.http.get<ProductoModel>(`${this.baseUrl}Productos/ObtenerProductoPorId`,{params});
  }

  BuscarPorPalabra(buscador:string):Observable<ProductoModel[]>{
    const params = new HttpParams()
    .set('Descripcion', buscador);
    return this.http.get<ProductoModel[]>(`${this.baseUrl}Productos/ListaProductosPorDescripcion`,{params});
  }
  ObtenerProductoPorCodigo(codigo:string):Observable<ProductoModel>{
    const params = new HttpParams()
    .set('codigo', codigo);
    return this.http.get<ProductoModel>(`${this.baseUrl}Productos/BusquedaPorCodigo`,{params});
  }
}
