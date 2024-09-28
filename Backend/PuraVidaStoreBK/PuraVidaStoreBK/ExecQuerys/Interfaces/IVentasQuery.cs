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
        Task<List<Factura>> facturasMes(int IdBodeg);
        Task<HistorialFacturasAnulada> ingresarHistorialNulas(HistorialFacturasAnulada facturaNula);
        Task<Factura> consultarFactura(string codigo);
        Task<List<DetalleFactura>> ConsultarDetallePorFactura(long idFactura);
        Task<Factura> ConsultarFormaPagoPorFactura(long idFactura);
        Factura GuardarFactura(Factura factura);

	}
}
