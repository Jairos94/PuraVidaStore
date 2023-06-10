using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Producto
{
    public int PrdId { get; set; }

    public string PrdNombre { get; set; } = null!;

    public double PrdPrecioVentaMayorista { get; set; }

    public double PrdPrecioVentaMinorista { get; set; }

    public string? PrdCodigo { get; set; }

    public int? PrdUnidadesMinimas { get; set; }

    public int PrdIdTipoProducto { get; set; }

    public string? PrdCodigoProvedor { get; set; }

    public bool? PdrVisible { get; set; }

    public string? PdrFoto { get; set; }

    public bool? PdrTieneExistencias { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<DetalleProductoPedido> DetalleProductoPedidos { get; set; } = new List<DetalleProductoPedido>();

    public virtual ICollection<HistorialPrecio> HistorialPrecios { get; set; } = new List<HistorialPrecio>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual TipoProducto PrdIdTipoProductoNavigation { get; set; } = null!;
}
