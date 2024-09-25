using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ImpuestosQuery : IImpuestosQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public ImpuestosQuery(PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<Impuesto> GuardarImpuesto(Impuesto impuesto)
        {
            try
            {
                
                    if (impuesto.ImpId > 0) 
                    {
                    dbContex.Impuestos.Update(impuesto);
                    }
                    else 
                    {
                        dbContex.Add(impuesto);
                    }
                   await dbContex.SaveChangesAsync();
                    return impuesto;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
           
        }

        public async Task<List<Impuesto>> ImpuestosPorDescripcion(string descripcion)
        {
            try
            {
                    var retorno =await dbContex.Impuestos.Where(x=>x.ImpDescripcion.Contains(descripcion) && x.ImpActivo == true ||
                                                              x.ImpId.ToString().Contains(descripcion) && x.ImpActivo == true ||
                                                              x.ImpActivo == true && x.ImpPorcentaje.ToString().Contains(descripcion)).ToListAsync() ;
                    return retorno;
                
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                throw;
            }
          
        }

        public async Task<Impuesto> ObtenerImpuestoPorId(int id)
        {
            try
            {
                
                    var retorno = await dbContex.Impuestos.FindAsync(id);
                    return retorno;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex); 
                throw;
            }
        }

        public async Task<List<Impuesto>> ObtenerListaImpuesto()
        {
            try
            {
                
                    var listaRetorno = await dbContex.Impuestos
                        .Where(x => x.ImpActivo == true)
                        .ToListAsync();

                    return listaRetorno;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex);
                throw;
            }
        }
    }
}
