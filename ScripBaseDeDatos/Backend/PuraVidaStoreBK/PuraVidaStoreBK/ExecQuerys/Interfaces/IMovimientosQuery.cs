using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IMovimientosQuery
    {
        public Task<List<Inventarios>> ListaInventarios(int IdBodega);

        public Task<List<Inventarios>> PorBusqueda(int IdBodega,string buscador);
        public Task<bool> IngresarProductosAlInventario(List<Inventarios> Inventarios, int IdBodega, int idUsuario, int Motivo);

    }
}
