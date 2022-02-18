using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuraVidaStoreApi.Model
{
    public class MPersona
    {
        public int PsrId { get; set; }
        public string PsrIdentificacion { get; set; }
        public string PsrNombre { get; set; }
        public string PsrApellido1 { get; set; }
        public string PsrApellido2 { get; set; }
    }
}
