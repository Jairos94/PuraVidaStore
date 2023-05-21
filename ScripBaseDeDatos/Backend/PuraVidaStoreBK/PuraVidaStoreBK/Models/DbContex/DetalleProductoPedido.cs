using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class DetalleProductoPedido
    {
        public int DppId { get; set; }
        public int DppIdProducto { get; set; }
        public int DppIdPedido { get; set; }
        public int DppIdMoneda { get; set; }
        public double? DppPesoUnitario { get; set; }
        public double? DppValorMoneda { get; set; }
        public double? DppCostoMoneda { get; set; }
        public double? DppCostoColones { get; set; }

        public virtual Moneda DppIdMonedaNavigation { get; set; } = null!;
        public virtual Pedido DppIdPedidoNavigation { get; set; } = null!;
        public virtual Producto DppIdProductoNavigation { get; set; } = null!;
    }
}
