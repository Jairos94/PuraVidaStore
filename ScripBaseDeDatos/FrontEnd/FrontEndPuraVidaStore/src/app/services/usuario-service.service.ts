import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UsuarioModel } from '../models/usuario-model';
import { Observable } from 'rxjs';
import { UsuarioRespuesta } from '../models/respuestas/usuario-respuesta';

@Injectable({
  providedIn: 'root'
})
export class UsuarioServiceService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  login(usario: string, contrasena: string): Observable<UsuarioRespuesta> {
    const params = new HttpParams()
      .set('user', usario)
      .set('password', contrasena);
    return this.http.get<UsuarioRespuesta>(`${this.baseUrl}Usuario/GetUsuario`, { params });
  }

  listaUsuarios(): Observable<UsuarioModel[]> {
    return this.http.get<UsuarioModel[]>(`${this.baseUrl}Usuario/ListaUsuarios`);
  }
  usuarioPorId(id: any): Observable<UsuarioModel> {
    const params = new HttpParams()
    .set('id', id);
    return this.http.get<UsuarioModel>(`${this.baseUrl}Usuario/UsuarioPorId`, { params });
  }

  EliminarUsuario(id:number):Observable<UsuarioModel>{
    const params = new HttpParams()
    .set('idUsuario', id);
    return this.http.delete<UsuarioModel>(`${this.baseUrl}Usuario/EliminarUsuario`, { params });
  }

  GuardarUsuario(usario:UsuarioModel,agregar:boolean):Observable<UsuarioModel>{
    const params= new HttpParams()
    .set('agregar',agregar);
    //const body =JSON.stringify(usario)
    return this.http.post<UsuarioModel>(`${this.baseUrl}Usuario/GuardarUsario`,usario,{ params });
  }
}
