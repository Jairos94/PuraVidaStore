import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { rutaGeneral } from './ruta.interface';



@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient, private ruta:rutaGeneral) { }



  
  get httpParams() {
    return new HttpParams().set('fields', 'user,password')
  }


  buscarUsuario(_Usuario:string,_pass:string,termino:string)
  {
    /onst url = `${this.ruta?.RutaGeneral}/${termino}`;
  }
}
