using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class HistorialClienteMayoristum
    {
        public int HcmId { get; set; }
        public int HcmIdCliente { get; set; }
        public DateTime HcmFechaVencimiento { get; set; }
        public DateTime HcmFechaActualizacion { get; set; }

        public virtual ClientesMayorista HcmIdClienteNavigation { get; set; } = null!;
    }
}
