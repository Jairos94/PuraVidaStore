using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class FacturaResumenDTO
    {
        public int FtrId { get; set; }
        public int FtrFactura { get; set; }
        public double FtrMontoTotal { get; set; }
        public double? FtrMontoPagado { get; set; }
        public double? FtrCambio { get; set; }

        public virtual FacturaDTO? FtrFacturaNavigation { get; set; }
    }
}
