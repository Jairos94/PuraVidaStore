using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class Usuario
{
    public int UsrId { get; set; }

    public string UsrUser { get; set; } = null!;

    public byte[] UsrPass { get; set; } = null!;

    public string? UsrEmail { get; set; }

    public int UsrIdRol { get; set; }

    public long UsrIdPersona { get; set; }

    public bool? UsrActivo { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HistorialFacturasAnulada> HistorialFacturasAnulada { get; set; } = new List<HistorialFacturasAnulada>();

    public virtual ICollection<HistorialPrecio> HistorialPrecios { get; set; } = new List<HistorialPrecio>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Persona UsrIdPersonaNavigation { get; set; } = null!;

    public virtual RolUsiario UsrIdRolNavigation { get; set; } = null!;

    public virtual UsuaiosEnvioCorreo? UsuaiosEnvioCorreo { get; set; }
}
