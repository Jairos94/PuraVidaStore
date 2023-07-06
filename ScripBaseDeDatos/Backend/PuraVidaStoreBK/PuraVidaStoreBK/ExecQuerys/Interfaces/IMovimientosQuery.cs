

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IMovimientosQuery
    {
        public Task<List<Inventarios>> ListaInventarios(int IdBodega);

        public Task<List<Inventarios>> PorBusqueda(int IdBodega,string buscador);
        public Task<bool> IngresarProductosAlInventario(List<Inventarios> Inventarios, int IdBodega, int idUsuario, int Motivo);
        public Task<MotivosMovimiento> GuardarMotivoMovimiento(MotivosMovimiento motivosMovimiento);
        public Task<List<MotivosMovimiento>> ObtenerListaMotivosMovimiento();
        public Task<List<MotivosMovimiento>> ObtenerListaMotivoMovimientoPorDescripcion(string descripcion);
        public Task<MotivosMovimiento> ObtenerMotivoMovmientoPorId(int id);
        public Task<bool> GuardarAjuste(Inventarios inventario, int IdBodega, int idUsuario, int Motivo);
        Task<List<MotivosMovimiento>> BusquedaPorDescripcion(string descripcion);
        Task<List<MotivosMovimiento>> GuardarListaMotivos(List<MotivosMovimiento> movimientos);
        Task<List<Movimiento>> GuardarListaMovimientos(List<Movimiento> movimientos);
        public Task<List<TipoMovimiento>> ObtenerListaTipoMovimiento(); 
    }
}
