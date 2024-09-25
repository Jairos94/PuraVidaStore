namespace PuraVidaStoreBK.Models.DTOS
{
    public class ReporteFacturaListaDTO
    {
        public string CodigoFactura { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoFactura { get; set; }
        public string DescripcionFactura { get; set; }
    }
}
