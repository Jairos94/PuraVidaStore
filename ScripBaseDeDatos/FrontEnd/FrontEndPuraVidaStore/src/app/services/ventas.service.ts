import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FormaPagoModel } from '../models/forma-pago-model';
import { FacturaModel } from '../models/factura-model';

@Injectable({
  providedIn: 'root'
})
export class VentasService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  listaFormaPago(): Observable<FormaPagoModel[]> {
    return this.http.get<FormaPagoModel[]>(`${this.baseUrl}Ventas/ObtenerFormasPago`);
  }

  IngresarVenta(factura:FacturaModel): Observable<FacturaModel> {
    return this.http.post<FacturaModel>(`${this.baseUrl}Ventas/IngresarVenta`,factura);
  }
}
