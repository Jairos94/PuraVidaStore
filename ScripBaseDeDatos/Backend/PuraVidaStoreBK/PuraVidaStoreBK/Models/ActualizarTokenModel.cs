namespace PuraVidaStoreBK.Models
{
    public class ActualizarTokenModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Creado { get; set; } = DateTime.Now;
        public DateTime FechaExpiro { get; set; } = DateTime.Now;
    }
}
