using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface ITipoProductoQuery
    {
        public Task<TipoMovimientos> ProductoPorId(int id);
        public Task<List<TipoMovimientos>> ListaProductoFiltrado();
        public Task<List<TipoMovimientos>> ListaTipoProducto();
        public Task<List<TipoMovimientos>> BuscarTipoProductoPorDescripcion(string busqueda);

        public Task<TipoMovimientos> Guardar(TipoMovimientos producto);

       

    }
}
