import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RolModel } from '../models/rol-model';

@Injectable({
  providedIn: 'root'
})
export class RolServiceService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  listaRoles():Observable<RolModel[]>{
    return this.http.get<RolModel[]>(`${this.baseUrl}Roles/ListaRoles`);
  }

}
