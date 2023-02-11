using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ImpuestosQuery : IImpuestosQuery
    {
        public async Task<Impuesto> GuardarImpuesto(Impuesto impuesto)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    if (impuesto.ImpId > 0) 
                    {
                        db.Impuestos.Update(impuesto);
                    }
                    else 
                    {
                        db.Add(impuesto);
                    }
                   await db.SaveChangesAsync();
                    return impuesto;
                }
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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    var retorno =await  db.Impuestos.Where(x=>x.ImpDescripcion.Contains(descripcion) && x.ImpActivo==true||
                                                              x.ImpId.ToString().Contains(descripcion) && x.ImpActivo == true ||
                                                              x.ImpActivo == true && x.ImpPorcentaje.ToString().Contains(descripcion)).ToListAsync() ;
                    return retorno;
                }
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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var retorno = await db.Impuestos.FindAsync(id);
                    return retorno;
                }
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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    var listaRetorno = await db.Impuestos
                        .Where(x => x.ImpActivo == true)
                        .ToListAsync();

                    return listaRetorno;
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
