import { ParametrosGlobalesModel } from "./parametros-globales-model";

export interface ParametrosEmailModel {
  preId: number;
  preHost: string;
  prePuerto: number;
  preUser: string;
  preClave: string;
  preSsl: boolean;
  preIdParametroGlobal: number | null;
  pre: ParametrosGlobalesModel;
}
