using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class FacturaDTO
    {
        public long FtrId { get; set; }

        public DateTime FtrFecha { get; set; }

        public int FtrIdUsuario { get; set; }

        public long? FtrMayorista { get; set; }

        public int FtrEstatusId { get; set; }

        public int FtrBodega { get; set; }

        public int FtrFormaPago { get; set; }

        public bool? FtrEsFacturaNula { get; set; }

        public string? FtrCodigoFactura { get; set; }
        public string? FtrCorreoEnvio { get; set; }

        public virtual ICollection<DetalleFacturaDTO>? DetalleFacturas { get; set; } = new List<DetalleFacturaDTO>();

        public virtual ICollection<FacturaResumenDTO>? FacturaResumen { get; set; } = new List<FacturaResumenDTO>();

        public virtual BodegaDTO? FtrBodegaNavigation { get; set; } = null!;

        public virtual EstatusFacturaDTO? FtrEstatus { get; set; } = null!;

        public virtual FormaPagoDTO? FtrFormaPagoNavigation { get; set; } = null!;

        public virtual UsuarioDto? FtrIdUsuarioNavigation { get; set; } = null!;

        public virtual ClienteMayoristaDTO? FtrMayoristaNavigation { get; set; }

        public virtual ICollection<HistorialClienteMayoristaDTO>? HistorialFacturasAnulada { get; set; } = new List<HistorialClienteMayoristaDTO>();

        public virtual ICollection<ImpuestosPorFacturaDTO>? ImpuestosPorFacturas { get; set; } = new List<ImpuestosPorFacturaDTO>();
    }
}
