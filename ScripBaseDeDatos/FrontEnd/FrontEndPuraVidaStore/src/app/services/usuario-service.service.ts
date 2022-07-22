import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UsuarioModel } from '../models/usuario-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioServiceService {
private baseUrl:string = environment.urlBase;

  constructor(private http: HttpClient) { }

   login(usario:string,contrasena:string):Observable<UsuarioModel>{
    const params = new HttpParams()
    .set('user', usario)
    .set('password', contrasena);
    return this.http.get<UsuarioModel>(`${this.baseUrl}Usuario/GetUsuario`,{params});
  }

}
