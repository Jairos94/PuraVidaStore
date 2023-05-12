

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IImpuestosQuery
    {
        Task<Impuesto> GuardarImpuesto(Impuesto impuesto);
        Task<Impuesto> ObtenerImpuestoPorId(int id);
        Task<List<Impuesto>> ObtenerListaImpuesto();
        Task<List<Impuesto>> ImpuestosPorDescripcion(string descripcion);
    }
}
