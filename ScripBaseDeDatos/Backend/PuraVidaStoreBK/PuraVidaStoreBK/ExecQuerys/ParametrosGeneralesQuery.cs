﻿using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ParametrosGeneralesQuery : IParametrosGeneralesQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public ParametrosGeneralesQuery(PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<ParametrosGlobales> GuardarParametros(ParametrosGlobales parametros)
        {
            parametros.ImpuestosPorParametros = null;
            try
            {
               
                    if (parametros.PrgId == 0)
                    {
                        dbContex.ParametrosGlobales.Add(parametros);
                    }
                    else
                    {
                        dbContex.ParametrosGlobales.Update(parametros);

                    }
                    await dbContex.SaveChangesAsync();

                    return parametros;
                
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
                  if (impuestos.ImpPid == 0)
                    {
                        var nuevoImpuesto = new ImpuestosPorParametro
                        {
                            ImpPidParametroGlobal = impuestos.ImpPidParametroGlobal,
                            ImpPidImpuesto = impuestos.ImpPidImpuesto
                        };
                    dbContex.ImpuestosPorParametros.Add(nuevoImpuesto);
                        await dbContex.SaveChangesAsync();

                        return nuevoImpuesto;
                    }
                    else
                    {
                        dbContex.ImpuestosPorParametros.Update(impuestos);
                        await dbContex.SaveChangesAsync();

                        return impuestos;

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
                
                    var retorno = await dbContex.ParametrosGlobales
                        .Where(x => x.PrgId == id)
                        .FirstAsync();

                    return retorno;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<List<ImpuestosPorParametro>> ObtenerImpuestosPorParametro(int id) 
        {
            
                return await dbContex.ImpuestosPorParametros.Where(x=>x.ImpPidParametroGlobal==id).Include(x=>x.ImpPidImpuestoNavigation).ToListAsync();
            
        }
    }
}
