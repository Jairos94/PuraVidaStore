import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PersonaModel } from '../models/persona-model';

@Injectable({
  providedIn: 'root'
})
export class PersonaServiceService {
  private baseUrl: string = environment.urlBase;
  constructor(
    private http: HttpClient
  ) { }

  listaPersonasCedula(cedula: string): Observable<PersonaModel[]> {
    const params = new HttpParams()
      .set('id', cedula);
    return this.http.get<PersonaModel[]>(`${this.baseUrl}persona/obtenerPersonaCedula`, { params });
  }

  buscarPersonaId(id:any):Observable<PersonaModel>{
    const params = new HttpParams()
    .set('id', id);
  return this.http.get<PersonaModel>(`${this.baseUrl}persona/personaPorId`, { params });
  }
}
