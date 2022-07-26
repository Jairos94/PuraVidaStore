using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class TipoMovimiento
    {
        public TipoMovimiento()
        {
            MotivosMovimientos = new HashSet<MotivosMovimiento>();
        }

        public int TpmId { get; set; }
        public string TpmDescripcion { get; set; } = null!;

        public virtual ICollection<MotivosMovimiento> MotivosMovimientos { get; set; }
    }
}
