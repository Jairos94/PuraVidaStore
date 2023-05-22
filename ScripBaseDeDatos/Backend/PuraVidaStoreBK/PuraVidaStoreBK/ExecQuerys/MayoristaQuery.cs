using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class MayoristaQuery : IMayoristaQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public MayoristaQuery(PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<ClientesMayorista> buscarClientePorCedulaOId(string buscador)
        {
			
            var Cliente = new ClientesMayorista();
			try
			{
				
					 
					Cliente = await dbContex.ClientesMayoristas
						.Where(x => x.ClmId.ToString() == buscador || x.ClmIdPersonaNavigation.PsrIdentificacion == buscador)
						.Include(x=>x.ClmIdPersonaNavigation)
						.FirstOrDefaultAsync();
				
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
                
					var consultaPersona = await dbContex.Personas.FindAsync(cliente.ClmIdPersonaNavigation.PsrId);
					if (consultaPersona==null) 
					{
                    dbContex.Personas.Add(cliente.ClmIdPersonaNavigation);
					}
					else
					{
                    dbContex.Personas.Update(cliente.ClmIdPersonaNavigation);
					}
					await dbContex.SaveChangesAsync();

					cliente.ClmIdPersona = cliente.ClmIdPersonaNavigation.PsrId;

					if (cliente.ClmId>0) 
					{
						dbContex.ClientesMayoristas.Update(cliente);
					}
					else
					{
                        dbContex.ClientesMayoristas.Add(cliente);
                    }
                   await dbContex.SaveChangesAsync();
					
					var ultimoHistorial = await dbContex.HistorialClienteMayorista
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

						dbContex.HistorialClienteMayorista.Add(nuevoRegistroHistorial);
						await dbContex.SaveChangesAsync();
					}

					return cliente;
                
            }
			catch (Exception ex)
			{
				Log.Error( ex.Message,ex);
				throw;
			}
			
        }
    }
}
