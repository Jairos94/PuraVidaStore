

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IBodegaQuery
    {
        Task<Bodega> GuardarBodega(Bodega bodega);
        Task<List<Bodega>> ListaBodegas();
        Task<List<Bodega>> ListaBodegasPorDescripcion(string Descripcion);
        Task<Bodega> BodegaPorId (int idBodega);

    }
}
