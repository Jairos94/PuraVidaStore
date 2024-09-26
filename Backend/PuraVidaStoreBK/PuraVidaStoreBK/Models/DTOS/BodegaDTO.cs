namespace PuraVidaStoreBK.Models.DTOS
{
    public class BodegaDTO
    {
        public int BdgId { get; set; }

        public string BdgDescripcion { get; set; } = null!;

        public bool? BdgVisible { get; set; }
    }
}
