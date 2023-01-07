using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
            FacturaResumen = new HashSet<FacturaResumen>();
            HistorialFacturasAnulada = new HashSet<HistorialFacturasAnulada>();
        }

        public int FtrId { get; set; }
        public DateTime FtrFecha { get; set; }
        public int FtrIdUsuario { get; set; }
        public int? FtrMayorista { get; set; }
        public int FtrEstatusId { get; set; }
        public int FtrBodega { get; set; }
        public int FtrFormaPago { get; set; }
        public bool? FtrEsFacturaNula { get; set; }
        public string? FtrCodigoFactura { get; set; }

        public virtual Bodega FtrBodegaNavigation { get; set; } = null!;
        public virtual EstatusFactura FtrEstatus { get; set; } = null!;
        public virtual FormaPago FtrFormaPagoNavigation { get; set; } = null!;
        public virtual Usuario FtrIdUsuarioNavigation { get; set; } = null!;
        public virtual ClientesMayorista? FtrMayoristaNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual ICollection<FacturaResumen> FacturaResumen { get; set; }
        public virtual ICollection<HistorialFacturasAnulada> HistorialFacturasAnulada { get; set; }
    }
}
