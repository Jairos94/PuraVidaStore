using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class HistorialClienteMayoristaDTO
    {
        public int HcmId { get; set; }
        public int HcmIdCliente { get; set; }
        public DateTime HcmFechaVencimiento { get; set; }
        public DateTime HcmFechaActualizacion { get; set; }

        public virtual ClienteMayoristaDTO? HcmIdClienteNavigation { get; set; } 
    }

}
