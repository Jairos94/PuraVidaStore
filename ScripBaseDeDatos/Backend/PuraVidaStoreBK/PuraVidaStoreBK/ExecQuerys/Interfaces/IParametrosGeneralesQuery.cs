
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IParametrosGeneralesQuery
    {
        public Task<ParametrosGlobales> GuardarParametros(ParametrosGlobales parametros);
        public Task<ParametrosGlobales> ObtenerParametrosId(int id);
    }
}
