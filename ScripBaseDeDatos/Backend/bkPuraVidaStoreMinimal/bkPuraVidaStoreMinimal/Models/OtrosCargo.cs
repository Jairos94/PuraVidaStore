using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class OtrosCargo
    {
        public int OtrId { get; set; }
        public int OtrIdMoneda { get; set; }
        public int OtrIdPedido { get; set; }
        public double? OtrValorMoneda { get; set; }
        public double? OtrCostoMoneda { get; set; }
        public string OtrRazon { get; set; } = null!;

        public virtual Monedum OtrIdMonedaNavigation { get; set; } = null!;
        public virtual Pedido OtrIdPedidoNavigation { get; set; } = null!;
    }
}
