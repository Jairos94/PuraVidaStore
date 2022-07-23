using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class MotivosMovimiento
    {
        public MotivosMovimiento()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int MtmId { get; set; }
        public string MtmDescripcion { get; set; } = null!;
        public int MtmIdTipoMovimiento { get; set; }

        public virtual TipoMovimiento MtmIdTipoMovimientoNavigation { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
