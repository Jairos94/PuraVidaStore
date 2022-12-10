using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IMovimientosQuery
    {
        public Task<Movimiento> IngresoDeProductosPorCompra(Movimiento movimiento);
        public Task<List<Inventarios>> ListaInventarios(int IdBodega);
    }
}
