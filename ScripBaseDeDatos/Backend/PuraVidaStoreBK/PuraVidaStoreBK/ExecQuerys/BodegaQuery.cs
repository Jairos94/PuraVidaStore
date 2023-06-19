using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using XAct.Library.Settings;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class BodegaQuery : IBodegaQuery
    {
        private readonly PuraVidaStoreContext dbContext;

        public BodegaQuery(PuraVidaStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<Bodega> BodegaPorId(int idBodega)
        {
            var bodega = new Bodega();
            try
            {
                    bodega = await dbContext.Bodegas.FindAsync(idBodega);
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
                    if (bodega.BdgId>0 && bodega != null) 
                    {
                         dbContext.Bodegas.Update(bodega);
                    }
                    else 
                    {
                         await dbContext.Bodegas.AddAsync(bodega);
                    }
                    await dbContext.SaveChangesAsync();
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
                lista = await dbContext.Bodegas.Where(x => x.BdgVisible == true).ToListAsync();
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
                lista= await dbContext.Bodegas
                    .Where(x => x.BdgVisible == true && x.BdgDescripcion.Contains(Descripcion))
                        .ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
            return lista;
        }
    }
}
