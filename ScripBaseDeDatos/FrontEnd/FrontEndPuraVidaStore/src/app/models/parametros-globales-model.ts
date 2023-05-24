import { ParametrosEmailModel } from "./parametros-email-model";
import { ParametrosImpuestoModel } from "./parametros-impuesto-model";

export interface ParametrosGlobalesModel {
    prgId: number;
    prgUndsHabilitarMayorista: number;
    prgUndsAgregarMayorista: number;
    prgHabilitarImpuestos: boolean;
    prgImpustosIncluidos: boolean;
    parametrosEmail: ParametrosEmailModel | null;
    impuestosPorParametros: ParametrosImpuestoModel[];
}
