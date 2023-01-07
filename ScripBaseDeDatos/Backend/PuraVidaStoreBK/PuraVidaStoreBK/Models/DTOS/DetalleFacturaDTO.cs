using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class DetalleFacturaDTO

    {
        public int DtfId { get; set; }
        public int DtfIdProducto { get; set; }
        public int DtfIdFactura { get; set; }
        public double DtfPrecio { get; set; }
        public int? DtfDescuento { get; set; }
        public int DtfCantidad { get; set; }

        public virtual ProductoDTO? DtfIdProducto1 { get; set; } 
        public virtual FacturaDTO? DtfIdProductoNavigation { get; set; } 
    }
}
