using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface ITipoProductoQuery
    {
        public Task<TipoProducto> ProductoPorId(int id);
        public Task<List<TipoProducto>> ListaProductoFiltrado();
        public Task<List<TipoProducto>> ListaTipoProducto();
        public Task<List<TipoProducto>> BuscarTipoProductoPorDescripcion(string busqueda);

        public Task<TipoProducto> Guardar(TipoProducto producto);

       

    }
}
