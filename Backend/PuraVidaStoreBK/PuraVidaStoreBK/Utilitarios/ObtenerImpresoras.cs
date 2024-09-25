using PuraVidaStoreBK.Utilitarios.Interfase;
using System.Drawing.Printing;

namespace PuraVidaStoreBK.Utilitarios
{
	public class ObtenerImpresoras: IObtenerImpresoras
	{
		public List<string> Impresoras() 
		{
			var lista = new List<string>();
			foreach (var nombre in PrinterSettings.InstalledPrinters) 
			{
				lista.Add(nombre.ToString());
			}

			return lista;
		}
	}
}
