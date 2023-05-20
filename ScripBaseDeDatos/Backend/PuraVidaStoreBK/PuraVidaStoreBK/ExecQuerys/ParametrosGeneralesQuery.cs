using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ParametrosGeneralesQuery : IParametrosGeneralesQuery
    {
        public async Task<ParametrosGlobales> GuardarParametros(ParametrosGlobales parametros)
        {
            parametros.ImpuestosPorParametros = null;
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

        public async Task<ImpuestosPorParametro> GuardarImpuestoPorParametro(ImpuestosPorParametro impuestos) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    if (impuestos.ImpPid == 0)
                    {
                        var nuevoImpuesto = new ImpuestosPorParametro
                        {
                            ImpPidParametroGlobal = impuestos.ImpPidParametroGlobal,
                            ImpPidImpuesto = impuestos.ImpPidImpuesto
                        };
                        db.ImpuestosPorParametros.Add(nuevoImpuesto);
                        await db.SaveChangesAsync();

                        return nuevoImpuesto;
                    }
                    else
                    {
                        db.ImpuestosPorParametros.Update(impuestos);
                        await db.SaveChangesAsync();

                        return impuestos;

                    }
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
                        .FirstAsync();

                    return retorno;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<List<ImpuestosPorParametro>> ObtenerImpuestosPorParametro(int id) 
        {
            using (PuraVidaStoreContext db= new PuraVidaStoreContext()) 
            {
                return await db.ImpuestosPorParametros.Where(x=>x.ImpPidParametroGlobal==id).Include(x=>x.ImpPidImpuestoNavigation).ToListAsync();
            }
        }
    }
}
