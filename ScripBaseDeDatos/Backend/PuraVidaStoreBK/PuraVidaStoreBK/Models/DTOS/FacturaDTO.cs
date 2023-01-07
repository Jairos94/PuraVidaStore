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
        public string? FtrCodigoFactura { get; set; }


        public virtual EstatusFacturaDTO? FtrEstatus { get; set; } 
        public virtual FormaPagoDTO? FtrFormaPagoNavigation { get; set; }
        public virtual UsuarioDto? FtrIdUsuarioNavigation { get; set; } 
        public virtual ClienteMayoristaDTO? FtrMayoristaNavigation { get; set; }
        public virtual ICollection<DetalleFacturaDTO>? DetalleFacturas { get; set; }
        public virtual ICollection<FacturaResumenDTO>? FacturaResumen { get; set; }
    }
}
