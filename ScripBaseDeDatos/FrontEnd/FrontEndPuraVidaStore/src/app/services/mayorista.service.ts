import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MayoristaModel } from '../models/mayorista-model';

@Injectable({
  providedIn: 'root',
})
export class MayoristaService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) {}

  obtenerMayoristaPorCedula(buscador:string) :Observable<MayoristaModel>
  {
    const params = new HttpParams().set('buscador', buscador);
    return this.http.get<MayoristaModel>(
      `${this.baseUrl}Mayorista/clienteMayoristaPorCedulaoId`,
      { params }
    );
  }

  obtenerListaMayorista():Observable<MayoristaModel[]>
  {
    return this.http.get<MayoristaModel[]>(
      `${this.baseUrl}Mayorista/ListaClienteMayorista`
    );
  }

  guardarMayorista(datos:MayoristaModel):Observable<MayoristaModel>{
    return this.http.post<MayoristaModel>(
      `${this.baseUrl}Mayorista/GuardarClienteMayorista`,datos
    );
  }
}
