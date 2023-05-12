using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using XAct;

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
                    if (parametros.PrgId == 0)
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
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<ParametrosGlobales> ObtenerParametrosId(int id)
        {

            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    var retorno = await db.ParametrosGlobales
                        .Where(x => x.PrgId == id)
                        .Include(x => x.ImpustosIncluidos)
                        .FirstAsync();

                    retorno.ImpustosIncluidos.ForEach(async x =>
                    {

                        var dato = await db.Impuestos.Where(z => z.ImpId == x.IicIdImpuesto).FirstAsync();
                        x.IicIdImpuestoNavigation = dato;
                    });

                    return retorno;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
