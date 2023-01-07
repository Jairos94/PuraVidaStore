using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class FacturaDTO
    {
        public int FtrId { get; set; }
        public DateTime FtrFecha { get; set; }
        public int FtrIdUsuario { get; set; }
        public int? FtrMayorista { get; set; }
        public int FtrEstatusId { get; set; }
        public int FtrBodega { get; set; }
        public int FtrFormaPago { get; set; }
        public bool? FtrEsFacturaNula { get; set; }

   
        public virtual EstatusFactura? FtrEstatus { get; set; } 
        public virtual FormaPago? FtrFormaPagoNavigation { get; set; }
        public virtual Usuario? FtrIdUsuarioNavigation { get; set; } 
        public virtual ClientesMayorista? FtrMayoristaNavigation { get; set; }
        public virtual ICollection<DetalleFactura>? DetalleFacturas { get; set; }
        public virtual ICollection<FacturaResumen>? FacturaResumen { get; set; }
    }
}
