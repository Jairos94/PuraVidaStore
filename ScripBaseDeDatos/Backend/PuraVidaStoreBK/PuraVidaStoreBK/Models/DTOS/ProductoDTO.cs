using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class ProductoDTO
    {
        public int PrdId { get; set; }
        public string PrdNombre { get; set; } = null!;
        public double PrdPrecioVentaMayorista { get; set; }
        public double PrdPrecioVentaMinorista { get; set; }
        public string? PrdCodigo { get; set; }
        public int? PrdUnidadesMinimas { get; set; }
        public int PrdIdTipoProducto { get; set; }
        public string? PrdCodigoProvedor { get; set; }
        public bool? PdrVisible { get; set; }
        public string? PdrFoto { get; set; }
        public bool? PdrTieneExistencias { get; set; }

        public virtual TipoProductoDTO? PrdIdTipoProductoNavigation { get; set; } 
    }
}
