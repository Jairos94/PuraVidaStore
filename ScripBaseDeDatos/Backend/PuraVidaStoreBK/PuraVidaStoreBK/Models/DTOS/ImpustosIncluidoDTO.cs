using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ImpustosIncluidoDTO
    {
        public int IicId { get; set; }
        public int? IicIdImpuesto { get; set; }
        public int? IicIdConfiguracion { get; set; }

        public virtual ParametrosGlobalesDTO? IicIdConfiguracionNavigation { get; set; }
        public virtual ImpuestosDTO? IicIdImpuestoNavigation { get; set; }
    }
}
