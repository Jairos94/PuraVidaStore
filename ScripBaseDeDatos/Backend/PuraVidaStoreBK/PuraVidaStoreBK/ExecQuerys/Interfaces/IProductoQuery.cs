using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IProductoQuery
    {
        public Task<List<Producto>> ListaProductosFiltrada();
        public Task<List<Producto>> ListaProductosNoFiltrada();
        public Task<List<Producto>> ListaProductos();
        public Task<Producto> ProductoPorId(int id);
        public Task<Producto> BuscarProductoPorCodigo(string codigo);
        public Task<List<Producto>> ProductoPorDescripcion(string Descripcion);
        public Task<List<Producto>> ProductoPorNoFiltradaDescripcion(string Descripcion);
        public Task<Producto> GuardarProducto(Producto producto, int idUsuario);
    }
}
