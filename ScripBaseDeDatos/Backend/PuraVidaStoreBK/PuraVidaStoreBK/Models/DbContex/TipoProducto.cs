using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class TipoMovimientos
    {
        public TipoMovimientos()
        {
            Productos = new HashSet<Producto>();
        }

        public int TppId { get; set; }
        public string TppDescripcion { get; set; } = null!;
        public bool? TppVisible { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
