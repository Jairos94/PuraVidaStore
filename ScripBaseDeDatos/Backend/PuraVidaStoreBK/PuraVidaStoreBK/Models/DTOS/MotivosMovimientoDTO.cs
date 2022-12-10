using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class MotivosMovimientoDTO
    {
        public int MtmId { get; set; }
        public string MtmDescripcion { get; set; } 
        public int MtmIdTipoMovimiento { get; set; }
        public virtual TipoMovimientoDTO? MtmIdTipoMovimientoNavigation { get; set; } 
    }
}
