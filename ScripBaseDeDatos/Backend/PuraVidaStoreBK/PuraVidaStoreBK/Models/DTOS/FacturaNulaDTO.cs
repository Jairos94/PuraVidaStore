namespace PuraVidaStoreBK.Models.DTOS
{
    public class EstructuraFacturaDTO
    {
        public string NumeroFctura { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoFactura { get; set; }
        public bool EsFacturaNula { get; set; }
        public decimal Total { get; set; }
    }
}
