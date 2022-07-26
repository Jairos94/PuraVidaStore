using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex2
{
    public partial class RolUsiario
    {
        public RolUsiario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int RluId { get; set; }
        public string RluDescripcion { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
