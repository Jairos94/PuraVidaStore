using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuraVidaStoreApi.Model
{
    public class MUsuario
    {
        public int UsrID { get; set; }
        public string UsrUser { get; set; }
        public string UsrPass { get; set; }
        public string UsrEmail { get; set; }
        public int UsrIdRol { get; set; }
        public int UsrIdPersona { get; set; }
    }
}
