using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public int TppId { get; set; }
        public string TppDescripcion { get; set; } = null!;
        public bool? TppVisible { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public static implicit operator TipoProducto(List<TipoProducto> v)
        {
            throw new NotImplementedException();
        }
    }
}
