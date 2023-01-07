using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IVentasQuery
    {
        public Task<Factura> ingresarFactura(Factura factura);
    }
}
