using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class Pedido
    {
        public Pedido()
        {
            DetalleProductoPedidos = new HashSet<DetalleProductoPedido>();
            OtrosCargos = new HashSet<OtrosCargo>();
            Trackins = new HashSet<Trackin>();
        }

        public int PddId { get; set; }
        public DateTime PddFecha { get; set; }
        public int PddIdUsuario { get; set; }
        public string? PddRazonCancelada { get; set; }
        public int PddProveedor { get; set; }
        public int PddEstado { get; set; }

        public virtual EstadoPedido PddEstadoNavigation { get; set; } = null!;
        public virtual Usuario PddIdUsuarioNavigation { get; set; } = null!;
        public virtual Proveedore PddProveedorNavigation { get; set; } = null!;
        public virtual ICollection<DetalleProductoPedido> DetalleProductoPedidos { get; set; }
        public virtual ICollection<OtrosCargo> OtrosCargos { get; set; }
        public virtual ICollection<Trackin> Trackins { get; set; }
    }
}
