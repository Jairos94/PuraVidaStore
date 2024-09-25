namespace PuraVidaStoreBK.Models.DTOS
{
    public class HistorialFacturasAnuladaDTO
    {
        public long HlfId { get; set; }

        public int HlfIdUsuario { get; set; }

        public long HlfIdFctura { get; set; }

        public string HlfRazon { get; set; } = null!;
    }
}
