namespace PuraVidaStoreBK.Models.DTOS
{
    public class TrasladoEntreBodegasDTO
    {
       
        public int idBodegaOrigen { get; set; }
        public int idBodegaDestino { get; set; }
        public int idUsuario { get; set; }
        public List<ProductosPorTrasladoDTO>? productos { get; set; }
    }
}
