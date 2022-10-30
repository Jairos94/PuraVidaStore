using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Usuario
    {
        public Usuario()
        {
            Facturas = new HashSet<Factura>();
            HistorialFacturasAnulada = new HashSet<HistorialFacturasAnulada>();
            HistorialPrecios = new HashSet<HistorialPrecio>();
            Movimientos = new HashSet<Movimiento>();
            Pedidos = new HashSet<Pedido>();
        }

        public int UsrId { get; set; }
        public string UsrUser { get; set; } = null!;
        public byte[] UsrPass { get; set; } = null!;
        public string? UsrEmail { get; set; }
        public int UsrIdRol { get; set; }
        public int UsrIdPersona { get; set; }
        public bool? UsrActivo { get; set; }

        public virtual Persona UsrIdPersonaNavigation { get; set; } = null!;
        public virtual RolUsiario UsrIdRolNavigation { get; set; } = null!;
        public virtual UsuaiosEnvioCorreo UsuaiosEnvioCorreo { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<HistorialFacturasAnulada> HistorialFacturasAnulada { get; set; }
        public virtual ICollection<HistorialPrecio> HistorialPrecios { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
