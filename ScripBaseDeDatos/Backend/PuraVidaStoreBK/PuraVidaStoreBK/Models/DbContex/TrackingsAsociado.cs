using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex;

public partial class TrackingsAsociado
{
    public long TraId { get; set; }

    public long TraIdTrackin { get; set; }

    public long TraIdTrackinPrincial { get; set; }

    public virtual Tracking TraIdTrackinNavigation { get; set; } = null!;
}
