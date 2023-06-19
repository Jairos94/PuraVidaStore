using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using XAct;

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

					var fecha= DateTime.Now;
					factura.FtrCodigoFactura=fecha.Year.ToString() + fecha.Month.ToString()+ factura.FtrId.ToString();
					dbContex.Facturas.Update(factura);

					factura.FacturaResumen.ForEach(x => 
					{
						dbContex.FacturaResumen.Add(x);
					});

					factura.DetalleFacturas.ForEach(x => 
					{
						x.DtfIdFactura = factura.FtrId;
					});


                    dbContex.DetalleFacturas.AddRange(factura.DetalleFacturas);

                    await dbContex.SaveChangesAsync();


                 
                
				
			}
			catch (Exception ex)
			{
				factura = new Factura();
				Log.Error(ex.Message,ex);
                
            }
            return factura;
        }

        public async Task<List<FormaPago>> listaFormaPago()
        {
			try
			{
				using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
				{
					return await db.FormaPagos.ToListAsync();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message, ex);
				return new List<FormaPago>();
			}
        }
    }
}
