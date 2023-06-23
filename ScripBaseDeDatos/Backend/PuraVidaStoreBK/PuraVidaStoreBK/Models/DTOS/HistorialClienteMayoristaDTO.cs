using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class HistorialClienteMayoristaDTO
    {
        public long HcmId { get; set; }

        public long HcmIdCliente { get; set; }

        public DateTime HcmFechaVencimiento { get; set; }

        public DateTime HcmFechaActualizacion { get; set; }

        public virtual ClienteMayoristaDTO? HcmIdClienteNavigation { get; set; } 
    }

}
