import { ImpuestosModel } from "./impuestos-model";
import { ParametrosGlobalesModel } from "./parametros-globales-model";

export interface ParametrosImpuestoModel {
  impPid: number;
  impPidParametroGlobal: number;
  impPidImpuesto: number;
  impPidImpuestoNavigation: ImpuestosModel | null;
  impPidParametroGlobalNavigation: ParametrosGlobalesModel | null;
}
