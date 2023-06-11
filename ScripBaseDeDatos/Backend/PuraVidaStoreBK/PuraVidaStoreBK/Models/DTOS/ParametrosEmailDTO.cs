

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ParametrosEmailDTO
    {
        public int PreId { get; set; }

        public string PreHost { get; set; } = null!;

        public int PrePuerto { get; set; }

        public string PreUser { get; set; } = null!;

        public string PreClave { get; set; } = null!;

        public bool PreSsl { get; set; }

        public int? PreIdParametroGlobal { get; set; }

        public virtual ParametrosGlobalesDTO Pre { get; set; } = null!;
    }
}
