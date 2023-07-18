using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ImpuestosPorFacturaDTO
    {
        public long IpfId { get; set; }

        public long IpfIdFactura { get; set; }

        public int IpfIdImpuesto { get; set; }

        public decimal? IpfPorcentaje { get; set; }
        public virtual ImpuestosDTO? IpfIdImpuestoNavigation { get; set; } 
    }
}
