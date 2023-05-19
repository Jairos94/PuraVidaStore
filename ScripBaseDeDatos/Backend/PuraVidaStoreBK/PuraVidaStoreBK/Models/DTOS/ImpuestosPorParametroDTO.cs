
namespace PuraVidaStoreBK.Models.DTOS
{
    public class ImpuestosPorParametroDTO
    {
        public long ImpPid { get; set; }
        public int? ImpPidParametroGlobal { get; set; }
        public int? ImpPidImpuesto { get; set; }

        public virtual ParametrosGlobalesDTO? ImpPidParametroGlobalNavigation { get; set; }
    }
}
