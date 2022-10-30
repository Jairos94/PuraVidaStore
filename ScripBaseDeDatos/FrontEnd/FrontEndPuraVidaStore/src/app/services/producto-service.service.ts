import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductoServiceService {
  private baseUrl: string = environment.urlBase;
  constructor(private http: HttpClient) { }

  
}
