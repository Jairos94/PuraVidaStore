using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Pedidos = new HashSet<Pedido>();
            Trackins = new HashSet<Trackin>();
        }

        public int PvdId { get; set; }
        public string PvdProveedorNmbre { get; set; } = null!;
        public string? PvdProveedorCorreo { get; set; }
        public string? PvdProveedorNumeroTelefono { get; set; }
        public int? PvdCodigoPais { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Trackin> Trackins { get; set; }
    }
}
