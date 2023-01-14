import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ImpuestosModel } from '../models/impuestos-model';

@Injectable({
  providedIn: 'root',
})

export class ImpuestoService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) {}

  listaImpuestos():Observable<ImpuestosModel[]>{
    return this.http.get<ImpuestosModel[]>(`${this.baseUrl}Impuesto/ObtenerListaImpuestos`);
  }

  guardarImpuesto(impuesto:ImpuestosModel):Observable<ImpuestosModel>{
    return this.http.post<ImpuestosModel>(`${this.baseUrl}Impuesto/GuardarImpuesto`,impuesto);
  }



  obtenerImpuestoId(id:number): Observable<ImpuestosModel> {
    const params = new HttpParams()
    .set('id', id);
    return this.http.get<ImpuestosModel>(`${this.baseUrl}Impuesto/ObtenerImpuestoporId`,{params});
  }


}
