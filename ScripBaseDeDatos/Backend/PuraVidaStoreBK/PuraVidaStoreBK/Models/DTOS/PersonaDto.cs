using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class PersonaDto
    {
        public long PsrId { get; set; }

        public string PsrIdentificacion { get; set; } = null!;

        public string PsrNombre { get; set; } = null!;

        public string PsrApellido1 { get; set; } = null!;

        public string PsrApellido2 { get; set; } = null!;

    }
}
