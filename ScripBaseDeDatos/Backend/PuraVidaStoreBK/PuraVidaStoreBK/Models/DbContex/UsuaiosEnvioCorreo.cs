using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class UsuaiosEnvioCorreo
{
    public int UecId { get; set; }

    public int UecIdUsuario { get; set; }

    public virtual Usuario Uec { get; set; } = null!;
}
