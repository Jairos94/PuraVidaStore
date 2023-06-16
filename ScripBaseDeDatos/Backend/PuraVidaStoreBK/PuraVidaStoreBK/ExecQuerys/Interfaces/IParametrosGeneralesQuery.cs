
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IParametrosGeneralesQuery
    {
         Task<ParametrosGlobales> GuardarParametros(ParametrosGlobales parametros);
        Task<ImpuestosPorParametro> GuardarImpuestoPorParametro(ImpuestosPorParametro impuestos);
        Task<List<ImpuestosPorParametro>> ObtenerImpuestosPorParametro(int id);
        Task<ParametrosEmail> GuardarEmail(ParametrosEmail parametrosEmail);
         Task<ParametrosGlobales> ObtenerParametrosId(int idBodega);
        Task<bool> EliminarImpustoPorParametro(List<ImpuestosPorParametro> datosElimnar);
    }
}
