using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class MovimientosDTO
    {
        public long MvmId { get; set; }

        public long MvmIdProducto { get; set; }

        public int MvmCantidad { get; set; }

        public DateTime MvmFecha { get; set; }

        public int MvmIdMotivoMovimiento { get; set; }

        public int MvmIdUsuario { get; set; }

        public int MvmIdBodega { get; set; }


        public virtual ProductoDTO? MvmIdProductoNavigation { get; set; }
        public virtual MotivosMovimientoDTO? MvmIdMotivoMovimientoNavigation { get; set; }
    }
}
