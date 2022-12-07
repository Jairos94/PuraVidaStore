using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Models.DTOS
{
    public class InventariosDTO
    {
        public ProductoDTO producto { get; set; }
        public int CantidadExistencia { get; set; }
    }
}

