namespace PuraVidaStoreBK.Models.DTOS
{
    public class ImpuestosDTO
    {
        public int ImpId { get; set; }

        public string? ImpDescripcion { get; set; }

        public decimal? ImpPorcentaje { get; set; }

        public bool? ImpActivo { get; set; }
    }
}
