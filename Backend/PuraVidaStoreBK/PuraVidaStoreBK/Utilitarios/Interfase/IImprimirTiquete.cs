using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Utilitarios.Interfase
{
	public interface IImprimirTiquete
	{
		void Imprimir(Factura factura, ParametrosGlobales parametros, bool esReimprimir);
	}
}
