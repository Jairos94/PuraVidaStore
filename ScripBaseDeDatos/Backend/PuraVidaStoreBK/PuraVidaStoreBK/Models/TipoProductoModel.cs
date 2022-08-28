namespace PuraVidaStoreBK.Models
{
    public class TipoProductoModel
    {
        public int TppId { get; set; }
        public string TppDescripcion { get; set; } = null!;
        public bool? TppVisible { get; set; }
    }
}
