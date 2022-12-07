import { ProductoModel } from "./producto-model";

export interface InventariosModel {
    producto: ProductoModel;
    cantidadExistencia: number;
}
