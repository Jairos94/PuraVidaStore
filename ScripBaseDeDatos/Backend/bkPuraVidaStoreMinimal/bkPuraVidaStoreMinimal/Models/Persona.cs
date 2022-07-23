using System;
using System.Collections.Generic;

namespace bkPuraVidaStoreMinimal.Models
{
    public partial class Persona
    {
        public Persona()
        {
            ClientesMayorista = new HashSet<ClientesMayorista>();
            Usuarios = new HashSet<Usuario>();
        }

        public int PsrId { get; set; }
        public string PsrIdentificacion { get; set; } = null!;
        public string PsrNombre { get; set; } = null!;
        public string PsrApellido1 { get; set; } = null!;
        public string PsrApellido2 { get; set; } = null!;

        public virtual ICollection<ClientesMayorista> ClientesMayorista { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
