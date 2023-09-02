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

        public async Task<List<DetalleFactura>> ObtenerDetallesFacturas(int idBodega, DateTime fechaInicio, DateTime fechaFin) 
        {
            var retorno = new List<DetalleFactura>();
            try
            {
                retorno = await _dbcontex.DetalleFacturas
                            .Include(x => x.DtfIdProducto1)
                            .Include(x => x.DtfIdProductoNavigation)
                                .ThenInclude(x => x.FtrIdUsuarioNavigation)
                            .Include(x => x.DtfIdProductoNavigation)
                                .ThenInclude(x => x.FtrBodegaNavigation)
                            .Where(x => x.DtfIdProductoNavigation.FtrBodega == idBodega &&
                                        x.DtfIdProductoNavigation.FtrFecha >= fechaInicio &&
                                        x.DtfIdProductoNavigation.FtrFecha <= fechaFin)
                            .ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return retorno;
        }

        public async Task<List<Movimiento>> ObtenerMovimientosProdcto(int idBodega, DateTime fechaInicio, DateTime fechaFin,string producto) 
        {
            var retorno = new List<Movimiento>();
            try
            {
                retorno = await _dbcontex.Movimientos
                                .Include(x => x.MvmIdMotivoMovimientoNavigation)
                                .Include(x => x.MvmIdBodegaNavigation)
                                .Include(x => x.MvmIdProductoNavigation)
                                .Include(x => x.MvmIdUsuarioNavigation)
                                .Where(x => x.MvmIdBodega == idBodega &&
                                       x.MvmFecha >= fechaInicio &&
                                       x.MvmFecha <= fechaFin && 
                                       (x.MvmIdProductoNavigation.PrdCodigo == producto || x.MvmIdProductoNavigation.PrdCodigoProvedor == producto))
                                .ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return retorno;
        }

        public async Task<List<Factura>> ObtenerReporteVentas(int idBodega, DateTime fechaInicio, DateTime fechaFin) 
        {
            var facturas = new List<Factura>();   
            try
            {
                facturas = await _dbcontex.Facturas
                                .Include(x=>x.FtrIdUsuarioNavigation)
                                .Include(x=>x.FtrBodegaNavigation)
                                .Include(x=>x.HistorialFacturasAnulada)
                                .Include(x=>x.FacturaResumen)
                                .Where(x=>
                                            x.FtrBodega==idBodega && 
                                            x.FtrFecha>=fechaInicio &&
                                            x.FtrFecha<=fechaFin
                                            )
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
               
            }
            return facturas;
        }
    }
}
