namespace PuraVidaStoreBK.Models
{
    public class ProductosModel
    {
        public int PrdId { get; set; }
        public string PrdNombre { get; set; } = null!;
        public double PrdPrecioVentaMayorista { get; set; }
        public double PrdPrecioVentaMinorista { get; set; }
        public string? PrdCodigo { get; set; }
        public int? PrdUnidadesMinimas { get; set; }
        public int PrdIdTipoProducto { get; set; }
        public string? PrdCodigoProvedor { get; set; }
        public byte[]? PrdFoto { get; set; }
        public bool? PdrVisible { get; set; }
    }
}
