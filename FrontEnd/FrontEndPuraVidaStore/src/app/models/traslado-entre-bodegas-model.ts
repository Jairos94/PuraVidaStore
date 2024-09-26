import { ProductoPorTrasladoModel } from "./producto-por-traslado-model";

export interface TrasladoEntreBodegasModel {
  idBodegaOrigen: number;
  idBodegaDestino: number;
  idUsuario: number;
  productos: ProductoPorTrasladoModel[] | null;
}
