using System;
using System.Collections.Generic;

namespace PuraVidaStoreBK.Models.DbContex
{
    public partial class ParametrosEmail
    {
        public int PreId { get; set; }
        public string PreHost { get; set; } = null!;
        public int PrePuerto { get; set; }
        public string PreUser { get; set; } = null!;
        public string PreClave { get; set; } = null!;
        public bool PreSsl { get; set; }
    }
}
