using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public int TppId { get; set; }
        public string TppDescripción { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
