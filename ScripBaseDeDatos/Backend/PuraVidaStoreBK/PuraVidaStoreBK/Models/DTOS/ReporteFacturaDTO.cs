namespace PuraVidaStoreBK.Models.DTOS
{
    public class ReporteFacturaDTO
    {
        public decimal MontoTotal { get; set; }
        public decimal MontoTotalNulas { get; set; }
        public List<ReporteFacturaListaDTO> Lista { get; set; }
    }
}
