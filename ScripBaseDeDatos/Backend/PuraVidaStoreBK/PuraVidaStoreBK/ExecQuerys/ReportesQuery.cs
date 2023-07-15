using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class ReportesQuery : IReportesQuery
    {
        private readonly PuraVidaStoreContext _dbcontex;

        public ReportesQuery(PuraVidaStoreContext dbcontex)
        {
            _dbcontex = dbcontex;
        }
        public async Task<List<Movimiento>> ObtenerMovimientosPorBodega(int idBodega, DateTime fechaInicio, DateTime fechaFin)
        {
            var retorno = new List<Movimiento>();
            try
            {
                retorno = await _dbcontex.Movimientos
                                .Include(x=>x.MvmIdMotivoMovimientoNavigation)
                                .Include(x=>x.MvmIdBodegaNavigation)
                                .Include(x=>x.MvmIdProductoNavigation)
                                .Include(x=>x.MvmIdUsuarioNavigation)
                                .Where(x=>x.MvmIdBodega ==idBodega && 
                                       x.MvmFecha>=fechaInicio && 
                                       x.MvmFecha<=fechaFin)
                                .ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return retorno;
        }

        public async Task<List<Factura>> ObtenerFacturasPorBodega(int idBodega, DateTime fechaInicio, DateTime fechaFin) 
        {
            var retorno = new List<Factura>();
            try
            {
                retorno = await _dbcontex.Facturas
                             .Include(x => x.FtrBodegaNavigation)
                             .Include(x => x.FtrIdUsuarioNavigation)
                             .Include(x => x.DetalleFacturas)
                             .ThenInclude(x=>x.DtfIdProducto1)
                             .Where(x => x.FtrBodega == idBodega &&
                                    x.FtrFecha >= fechaInicio &&
                                    x.FtrFecha <= fechaFin)
                             .ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return retorno;
        }
    }
}
