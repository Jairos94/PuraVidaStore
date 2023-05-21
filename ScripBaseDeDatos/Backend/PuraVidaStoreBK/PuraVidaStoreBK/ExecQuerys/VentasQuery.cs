using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;
using XAct;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class VentasQuery : IVentasQuery
    {
        public async Task<Factura> ingresarFactura(Factura factura)
        {
			try
			{
				using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
				{
					db.Facturas.Add(factura);
					await db.SaveChangesAsync();

					var fecha= DateTime.Now;
					factura.FtrCodigoFactura=fecha.Year.ToString() + fecha.Month.ToString()+ factura.FtrId.ToString();
					db.Facturas.Update(factura);

					factura.FacturaResumen.ForEach(x => 
					{
						db.FacturaResumen.Add(x);
					});

					factura.DetalleFacturas.ForEach(x => 
					{
						x.DtfIdFactura = factura.FtrId;
					});


                    db.DetalleFacturas.AddRange(factura.DetalleFacturas);

                    await db.SaveChangesAsync();


                 
                }
				
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
