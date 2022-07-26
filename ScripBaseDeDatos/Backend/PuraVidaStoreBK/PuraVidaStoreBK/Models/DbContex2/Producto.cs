using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
            DetalleProductoPedidos = new HashSet<DetalleProductoPedido>();
            HistorialPrecios = new HashSet<HistorialPrecio>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int PrdId { get; set; }
        public string PrdNombre { get; set; } = null!;
        public double PrdPrecioVentaMayorista { get; set; }
        public double PrdPrecioVentaMinorista { get; set; }
        public string? PrdCodigo { get; set; }
        public int? PrdUnidadesMinimas { get; set; }
        public int PrdIdTipoProducto { get; set; }
        public byte[]? PrdFoto { get; set; }
        public string? PrdCodigoProvedor { get; set; }

        public virtual TipoProducto PrdIdTipoProductoNavigation { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual ICollection<DetalleProductoPedido> DetalleProductoPedidos { get; set; }
        public virtual ICollection<HistorialPrecio> HistorialPrecios { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
