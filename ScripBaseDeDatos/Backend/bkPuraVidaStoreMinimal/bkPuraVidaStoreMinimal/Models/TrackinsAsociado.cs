using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class TrackinsAsociado
    {
        public int TraId { get; set; }
        public int TraIdTrackin { get; set; }
        public int TraIdTrackinPrincial { get; set; }

        public virtual Trackin TraIdTrackinNavigation { get; set; } = null!;
    }
}
