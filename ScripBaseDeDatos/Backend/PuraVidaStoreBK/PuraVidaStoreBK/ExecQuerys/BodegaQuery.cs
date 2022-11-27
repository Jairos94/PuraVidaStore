using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class BodegaQuery : IBodegaQuery
    {
        public async Task<Bodega> BodegaPorId(int idBodega)
        {
            var bodega = new Bodega();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    bodega = await db.Bodegas.FindAsync(idBodega);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return bodega;
        }

        public async Task<Bodega> GuardarBodega(Bodega bodega)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    if (bodega.BdgId>0 && bodega != null) 
                    {
                         db.Bodegas.Update(bodega);
                    }
                    else 
                    {
                         await db.Bodegas.AddAsync(bodega);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                
                Log.Error(ex.Message);
                bodega = null;
            }
            return bodega;
        }

        public async Task<List<Bodega>> ListaBodegas()
        {
            var lista = new List<Bodega>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    lista =await db.Bodegas.Where(x=>x.BdgVisible==true).ToListAsync();
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return lista;
        }

        public async Task<List<Bodega>> ListaBodegasPorDescripcion(string Descripcion)
        {
            var lista = new List<Bodega>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    lista = await db.Bodegas
                        .Where(x => x.BdgVisible == true && x.BdgDescripción.Contains(Descripcion))
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return lista;
        }
    }
}
