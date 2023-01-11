using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ParametrosGeneralesQuery : IParametrosGeneralesQuery
    {
        public async Task<ParametrosGlobales> GuardarParametros(ParametrosGlobales parametros)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    if (parametros.PrgId>0) 
                    {
                      db.ParametrosGlobales.Add(parametros);
                    }
                    else 
                    {
                        db.ParametrosGlobales.Update(parametros);

                    }
                    await db.SaveChangesAsync();
                    return parametros;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex);
                throw;
            }
        }

        public async Task<ParametrosGlobales> ObtenerParametrosId(int id)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                   var retorno = await db.ParametrosGlobales.FindAsync(id);
                    return retorno;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex);
                throw;
            }
        }
    }
}
