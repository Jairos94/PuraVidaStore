using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IReportesQuery
    {
        Task<List<Movimiento>> ObtenerMovimientosPorBodega(int idBodega, DateTime fechaInicio, DateTime fechaFin);
        Task<List<Factura>> ObtenerFacturasPorBodega(int idBodega, DateTime fechaInicio, DateTime fechaFin);
    }

}
