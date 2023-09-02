using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class VentasQuery : IVentasQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public VentasQuery( PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<Factura> ingresarFactura(Factura factura)
        {
			try
			{
				dbContex.Facturas.Add(factura);
				await dbContex.SaveChangesAsync();
				
			}
			catch (Exception ex)
			{
				factura = new Factura();
				Log.Error(ex.Message,ex);
                
            }
            return factura;
        }

		public async Task<Factura> actualizarFactura(Factura factura) 
		{
			try
			{
				dbContex.Facturas.Update(factura);
				await dbContex.SaveChangesAsync();
				
			}
			catch (Exception ex)
			{

                factura = new Factura();
                Log.Error(ex.Message, ex);
            }
            return factura;
        }


		public async Task<FacturaResumen> ingresarFacturaResumen(FacturaResumen facturaResumen) 
		{
            try
            {
                dbContex.FacturaResumen.Add(facturaResumen);
                await dbContex.SaveChangesAsync();
         

            }
            catch (Exception ex)
            {
                facturaResumen = new FacturaResumen();
                Log.Error(ex.Message, ex);

            }
            return facturaResumen;
        }

        public async Task<List<DetalleFactura>> ingresarDetalleFactura(List<DetalleFactura> listaDetalleFactura) 
        {
            try
            {
                dbContex.DetalleFacturas.AddRange(listaDetalleFactura);
                await dbContex.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                listaDetalleFactura = new List<DetalleFactura>();
                Log.Error(ex.Message, ex);

            }
            return listaDetalleFactura;
        }

        public async Task<List<ImpuestosPorFactura>> ingresarImpuestosPorFactura(List<ImpuestosPorFactura> impuestos)
        {
            try
            {
                dbContex.ImpuestosPorFacturas.AddRange(impuestos);
                await dbContex.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                impuestos = new List<ImpuestosPorFactura>();
                Log.Error(ex.Message, ex);

            }
            return impuestos;
        }


        public async Task<Factura> buscarFacturaPorCodigo(string buscador)
        {
            try
            {
             return await  dbContex.Facturas.Where(x=>x.FtrCodigoFactura==buscador)
                    .Include(x => x.FacturaResumen)
                    .Include(x => x.DetalleFacturas)
                    .Include(x => x.ImpuestosPorFacturas)
                    .FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {
                
                Log.Error(ex.Message, ex);
                return new Factura();

            }
        }

        public async Task<List<FormaPago>> listaFormaPago()
        {
			try
			{
				
					return await dbContex.FormaPagos.ToListAsync();
				
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, ex);
				return new List<FormaPago>();
			}
        }

        public async Task<List<Factura>> facturasMes(int IdBodega) 
        {
            var listaFacturas  =new List<Factura>();
            try
            {
                var fechaActual =  DateTime.Now;
                listaFacturas = await dbContex.Facturas
                                      .Include(x=>x.FtrIdUsuarioNavigation)
                                      .Include(x=>x.FacturaResumen)
                                      //.Include(x=>x.FtrMayoristaNavigation)
                                      //.Include(x => x.DetalleFacturas).ThenInclude(x=>x.DtfIdProducto1)
                                      .Where(x => x.FtrBodega==IdBodega &&
                                                  x.FtrFecha.Year == fechaActual.Year && 
                                                  x.FtrFecha.Month == fechaActual.Month).ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return listaFacturas;
        }
        public async Task<HistorialFacturasAnulada> ingresarHistorialNulas(HistorialFacturasAnulada facturaNula) 
        {
            try
            {
                dbContex.HistorialFacturasAnuladas.Add(facturaNula);
                await dbContex.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                facturaNula = new HistorialFacturasAnulada();
                Log.Error(ex.StackTrace);
            }
            return facturaNula;
        }
        public async Task<Factura> consultarFactura(string codigo) 
        {
            var retorno = new Factura();
            try
            {
                retorno = await dbContex.Facturas
                    .Include(x => x.FacturaResumen)
                    .Include(x => x.FtrMayoristaNavigation).ThenInclude(x=>x.ClmIdPersonaNavigation)
                    .Include(x => x.ImpuestosPorFacturas).ThenInclude(x => x.IpfIdImpuestoNavigation)
                    .Include(x => x.FtrIdUsuarioNavigation)
                    //.Include(x => x.HistorialFacturasAnulada)
                    .Where(x=>x.FtrCodigoFactura== codigo ||
                              x.FtrId.ToString()==codigo)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return retorno;
        }

        public async Task<List<DetalleFactura>> ConsultarDetallePorFactura(long idFactura) 
        {
            var listaDetalles= new List<DetalleFactura>();
            try
            {
                listaDetalles = await dbContex.DetalleFacturas
                                    .Include(x=>x.DtfIdProducto1)
                                    .Where(x=>x.DtfIdFactura== idFactura).ToListAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return listaDetalles;
        }

        public async Task<Factura> ConsultarFormaPagoPorFactura(long idFactura) 
        {
            var retorno = new Factura();
            try
            {
                 retorno = await dbContex.Facturas
                    .Include(x => x.FtrFormaPagoNavigation)
                    .Include(x => x.FtrBodegaNavigation)
                    .Where(x =>  x.FtrId== idFactura)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.StackTrace);
            }
            return retorno;
        }


    }
}
