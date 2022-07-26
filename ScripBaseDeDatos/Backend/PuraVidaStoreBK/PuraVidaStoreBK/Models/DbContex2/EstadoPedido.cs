using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class EstadoPedido
    {
        public EstadoPedido()
        {
            Pedidos = new HashSet<Pedido>();
            Trackins = new HashSet<Trackin>();
        }

        public int EtpId { get; set; }
        public string EtpDescripcion { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Trackin> Trackins { get; set; }
    }
}
