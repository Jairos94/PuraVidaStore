using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Utilitarios.Interfase
{
    public interface IEnvioCorreo
    {
        void EnviarFactura(Factura factura, ParametrosEmail email, List<string> correosDestinatarios);
    }
}
