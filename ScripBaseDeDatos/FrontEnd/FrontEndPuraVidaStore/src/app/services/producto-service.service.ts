import { HttpClient } from '@angular/common/http';
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

  GuardarProducto(Producto:ProductoModel):Observable<ProductoModel>{
    return this.http.post<ProductoModel>(`${this.baseUrl}TipoProduco/GuardarTipoProducto`,Producto);
  }
}
