


namespace PuraVidaStoreBK.Models.DTOS
{
    public class ImpuestosPorParametroDTO
    {
        public int ImpPid { get; set; }

        public int ImpPidParametroGlobal { get; set; }

        public int ImpPidImpuesto { get; set; }

        public virtual ImpuestosDTO? ImpPidImpuestoNavigation { get; set; } = null!;
        public virtual ParametrosGlobalesDTO? ImpPidParametroGlobalNavigation { get; set; } = null!;
    }
}
