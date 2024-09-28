import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FormaPagoModel } from '../models/forma-pago-model';
import { FacturaModel } from '../models/factura-model';
import { EstructuraFacturaModel } from '../models/estructura-factura-model';
import { HistorialFacturasNulasModel } from '../models/historial-facturas-nulas-model';
import { Injectable } from '@angular/core';

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

  FacturasMes(IdBodega:number):Observable<EstructuraFacturaModel[]>{
    const params = new HttpParams()
    .set('IdBodega', IdBodega);
    return this.http.get<EstructuraFacturaModel[]>(`${this.baseUrl}Ventas/ObtenerFacturasDelMes`,{params});

  }

  FacturasPorCodigo(Codigo:string):Observable<FacturaModel>{
    const params = new HttpParams()
    .set('codigo', Codigo);
    return this.http.get<FacturaModel>(`${this.baseUrl}Ventas/ObtenerFacturaPorCodigo`,{params});

  }


  AnularFacturas(facturaNula:HistorialFacturasNulasModel):Observable<HistorialFacturasNulasModel>{


    return this.http.post<HistorialFacturasNulasModel>(`${this.baseUrl}Ventas/FacturaNula`,facturaNula);

  }

  ReenviarFactura(factura:string):Observable<string>{
    const params = new HttpParams()
    .set('idFactura', factura)
    return this.http.get<string>(`${this.baseUrl}Ventas/ReenviarFactura`,{params});
  }
}
