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

        public virtual ICollection<ImpuestosDTO> ImpustosIncluidos { get; set; }
    }
}
