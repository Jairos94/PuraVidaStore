

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ParametrosGlobalesDTO
    {
        public int PrgId { get; set; }

        public int PrgUndsHabilitarMayorista { get; set; }

        public int PrgUndsAgregarMayorista { get; set; }

        public bool PrgHabilitarImpuestos { get; set; }

        public bool PrgImpustosIncluidos { get; set; }

        public int PrgIdBodega { get; set; }

        public virtual ICollection<ImpuestosPorParametroDTO>? ImpuestosPorParametros { get; set; } = new List<ImpuestosPorParametroDTO>();

        public virtual ParametrosEmailDTO? ParametrosEmail { get; set; }

        public virtual BodegaDTO? PrgIdBodegaNavigation { get; set; } = null!;
    }
}
