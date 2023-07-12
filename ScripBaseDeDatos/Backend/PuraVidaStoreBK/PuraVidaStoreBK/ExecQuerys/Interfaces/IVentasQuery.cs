using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IVentasQuery
    {
        Task<Factura> ingresarFactura(Factura factura);
        Task<Factura> actualizarFactura(Factura factura);
        Task<FacturaResumen> ingresarFacturaResumen(FacturaResumen facturaResumen);
        Task<List<DetalleFactura>> ingresarDetalleFactura(List<DetalleFactura> listaDetalleFactura);
        Task<List<ImpuestosPorFactura>> ingresarImpuestosPorFactura(List<ImpuestosPorFactura> impuestos);
        Task<Factura> buscarFacturaPorCodigo(string buscador);
        Task<List<FormaPago>> listaFormaPago();
    }
}
