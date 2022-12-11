import { TipoProductoModel } from "./tipo-producto";

export interface ProductoModel {
    prdId: number;
    prdNombre: string;
    prdPrecioVentaMayorista: number;
    prdPrecioVentaMinorista: number;
    prdCodigo: string | null;
    prdUnidadesMinimas: number | null;
    prdIdTipoProducto: number;
    prdCodigoProvedor: string | null;
    pdrVisible: boolean | null;
    pdrFoto: string | null;
    pdrTieneExistencias: boolean | null;
    prdIdTipoProductoNavigation:TipoProductoModel  | null;
}
