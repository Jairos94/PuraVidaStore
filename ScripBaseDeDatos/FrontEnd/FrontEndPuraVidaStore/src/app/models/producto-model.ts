export interface ProductoModel {
    prdId: number;
    prdNombre: string;
    prdPrecioVentaMayorista: number;
    prdPrecioVentaMinorista: number;
    prdCodigo: string | null;
    prdUnidadesMinimas: number | null;
    prdIdTipoProducto: number;
    prdCodigoProvedor: string | null;
    prdFoto: any| null;
    pdrVisible: boolean | null;
}
