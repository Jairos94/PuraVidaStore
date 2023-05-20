
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IParametrosGeneralesQuery
    {
        public Task<ParametrosGlobales> GuardarParametros(ParametrosGlobales parametros);
        Task<ImpuestosPorParametro> GuardarImpuestoPorParametro(ImpuestosPorParametro impuestos);
        Task<List<ImpuestosPorParametro>> ObtenerImpuestosPorParametro(int id);
        public Task<ParametrosGlobales> ObtenerParametrosId(int id);
    }
}
