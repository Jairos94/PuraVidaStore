using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class DetalleFacturaDTO

    {
        public long DtfId { get; set; }

        public long DtfIdProducto { get; set; }

        public long DtfIdFactura { get; set; }

        public double DtfPrecio { get; set; }

        public int? DtfDescuento { get; set; }

        public int? DtfCantidad { get; set; }

        public virtual ProductoDTO? DtfIdProducto1 { get; set; } 
        public virtual FacturaDTO? DtfIdProductoNavigation { get; set; } 
    }
}
