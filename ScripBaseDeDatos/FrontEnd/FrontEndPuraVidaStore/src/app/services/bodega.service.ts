import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BodegaModel } from '../models/bodega-model';

@Injectable({
  providedIn: 'root'
})
export class BodegaService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) { }

  listaUsuarios(): Observable<BodegaModel[]> {
    return this.http.get<BodegaModel[]>(`${this.baseUrl}Bodega/ListaBodegas`);
  }
}
