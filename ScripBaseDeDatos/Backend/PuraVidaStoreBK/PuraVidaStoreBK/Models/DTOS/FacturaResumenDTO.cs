using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class FacturaResumenDTO
    {
        public long FtrId { get; set; }

        public long FtrFactura { get; set; }

        public decimal FtrMontoTotal { get; set; }

        public decimal FtrMontoImpuestos { get; set; }

        public decimal? FtrMontoPagado { get; set; }

        public decimal? FtrCambio { get; set; }


        public virtual FacturaDTO? FtrFacturaNavigation { get; set; }
    }
}
