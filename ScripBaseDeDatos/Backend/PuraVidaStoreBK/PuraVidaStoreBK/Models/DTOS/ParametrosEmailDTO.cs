namespace PuraVidaStoreBK.Models.DTOS
{
    public class ParametrosEmailDTO
    {
        public int PreId { get; set; }
        public string PreHost { get; set; }
        public int PrePuerto { get; set; }
        public string PreUser { get; set; } 
        public string PreClave { get; set; }
        public bool PreSsl { get; set; }
    }
}
