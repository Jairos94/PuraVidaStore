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
					.Include(x => x.ClmIdPersonaNavigation)
					.Include(x=>x.HistorialClienteMayorista)
					.FirstOrDefaultAsync();

			}
			catch (Exception ex)
			{

				Log.Error(ex.Message, ex);
			}
			return Cliente;
		}

		public async Task<ClientesMayorista> guardarClienteMayorista(ClientesMayorista cliente)
		{

			try
			{
				if (cliente.ClmId>0) 
				{
					dbContex.Update(cliente);
				}
				else 
				{
					dbContex.Add(cliente);
				}
				await dbContex.SaveChangesAsync();
				return cliente;
			}
			catch (Exception ex)
			{

				Log.Error(ex.StackTrace);
				return new ClientesMayorista();
			}

		}

		public async Task<List<ClientesMayorista>> listaClientesMayorista()
		{
			return await dbContex.ClientesMayoristas
				         .Include(x=>x.ClmIdPersonaNavigation) 
						 .ToListAsync();	
		}

		public async Task<HistorialClienteMayorista> AgregarAlhistorial(HistorialClienteMayorista historial) 
		{
			await dbContex.HistorialClienteMayorista.AddAsync(historial);
			 dbContex.SaveChanges();
			return historial;
		}
    }
}
