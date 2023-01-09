using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class MayoristaQuery : IMayoristaQuery
    {
        public async Task<ClientesMayorista> buscarClientePorCedulaOId(string buscador)
        {
			var Cliente = new ClientesMayorista();
			try
			{
				using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
				{
					Cliente= await db.ClientesMayoristas
						.Where(x => x.ClmIdPersona.ToString()==buscador || x.ClmIdPersonaNavigation.PsrIdentificacion == buscador)
						.Include(x=>x.ClmIdPersonaNavigation)
						.FirstOrDefaultAsync();
				}
			}
			catch (Exception ex)
			{

				Log.Error(ex.Message,ex);
			}
			return Cliente;
        }

        public async Task<ClientesMayorista> guardarClienteMayorista(ClientesMayorista cliente)
        {
			try
			{
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
					var consultaPersona = await db.Personas.FindAsync(cliente.ClmIdPersonaNavigation.PsrId);
					if (consultaPersona==null) 
					{
						db.Personas.Add(cliente.ClmIdPersonaNavigation);
					}
					else
					{
						db.Personas.Update(cliente.ClmIdPersonaNavigation);
					}
					await db.SaveChangesAsync();

					cliente.ClmIdPersona = cliente.ClmIdPersonaNavigation.PsrId;

					if (cliente.ClmId>0) 
					{
						db.ClientesMayoristas.Update(cliente);
					}
					else
					{
                        db.ClientesMayoristas.Add(cliente);
                    }
                   await db.SaveChangesAsync();
					
					var ultimoHistorial = await db.HistorialClienteMayorista
						                  .Where(x=>x.HcmIdCliente==cliente.ClmId)
										  .OrderByDescending(x=>x.HcmFechaActualizacion)
										  .FirstAsync();
					
					if (ultimoHistorial.HcmFechaVencimiento<DateTime.Now || ultimoHistorial ==null) 
					{
						HistorialClienteMayorista nuevoRegistroHistorial = new HistorialClienteMayorista
						{
							HcmId = 0,
							HcmFechaActualizacion = cliente.ClmFechaCreacion,
							HcmFechaVencimiento = cliente.ClmFechaVencimiento,
							HcmIdCliente = cliente.ClmId
						};

						db.HistorialClienteMayorista.Add(nuevoRegistroHistorial);
						await db.SaveChangesAsync();
					}

					return cliente;
                }
            }
			catch (Exception ex)
			{
				Log.Error( ex.Message,ex);
				throw;
			}
			
        }
    }
}
