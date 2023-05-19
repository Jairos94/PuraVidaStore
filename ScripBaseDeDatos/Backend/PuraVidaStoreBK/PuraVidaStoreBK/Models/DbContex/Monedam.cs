using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class Monedam
    {
        public Monedam()
        {
            DetalleProductoPedidos = new HashSet<DetalleProductoPedido>();
            OtrosCargos = new HashSet<OtrosCargo>();
            Trackins = new HashSet<Trackin>();
        }

        public int MndId { get; set; }
        public string MndCodigo { get; set; } = null!;
        public string MndDescripcion { get; set; } = null!;

        public virtual ICollection<DetalleProductoPedido> DetalleProductoPedidos { get; set; }
        public virtual ICollection<OtrosCargo> OtrosCargos { get; set; }
        public virtual ICollection<Trackin> Trackins { get; set; }
    }
}
