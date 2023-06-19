import { BodegaModel } from "./bodega-model";
import { ParametrosEmailModel } from "./parametros-email-model";
import { ParametrosImpuestoModel } from "./parametros-impuesto-model";

export interface ParametrosGlobalesModel {
  prgId: number;
  prgUndsHabilitarMayorista: number;
  prgUndsAgregarMayorista: number;
  prgHabilitarImpuestos: boolean;
  prgImpustosIncluidos: boolean;
  prgIdBodega: number;
  impuestosPorParametros: ParametrosImpuestoModel[] | null;
  parametrosEmail: ParametrosEmailModel  | null;
  prgIdBodegaNavigation: BodegaModel | null;
}
