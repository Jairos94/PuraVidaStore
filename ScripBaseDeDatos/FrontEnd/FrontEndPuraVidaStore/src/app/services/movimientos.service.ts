import { activo } from './../activo';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InventariosModel } from '../models/inventarios-model';
import { MotivoMovimientoModel } from '../models/motivo-movimiento-model';

@Injectable({
  providedIn: 'root',
})
export class MovimientosService {
  private baseUrl: string = environment.urlBase;

  constructor(private http: HttpClient) {}

  ProductosExistencias(idBodega: number): Observable<InventariosModel[]> {
    const params = new HttpParams().set('IdBodega', idBodega);
    return this.http.get<InventariosModel[]>(
      `${this.baseUrl}Movimientos/Inventarios`,
      { params }
    );
  }

  Sugerencias(
    idBodega: number,
    buscador: string
  ): Observable<InventariosModel[]> {
    const params = new HttpParams()
      .set('IdBodega', idBodega)
      .set('Buscador', buscador);
    return this.http.get<InventariosModel[]>(
      `${this.baseUrl}Movimientos/Busqueda`,
      { params }
    );
  }

  GuardarSinOrden(
    ProductosAIngresar: InventariosModel[]
  ): Observable<InventariosModel[]> {
    const params = new HttpParams()
      .set('IdBodega', activo.bodegaIngreso.bdgId)
      .set('IdUsuario', activo.usuarioPrograma.usrId)
      .set('Motivo', 1);

    return this.http.post<InventariosModel[]>(
      `${this.baseUrl}Movimientos/IngresarProductosAlInventario`,
      ProductosAIngresar,
      { params }
    );
  }

  obtenerMotivosMovimientos(): Observable<MotivoMovimientoModel[]> {
    return this.http.get<MotivoMovimientoModel[]>(
      `${this.baseUrl}Movimientos/ObtenerMotivos`
    );
  }

}
