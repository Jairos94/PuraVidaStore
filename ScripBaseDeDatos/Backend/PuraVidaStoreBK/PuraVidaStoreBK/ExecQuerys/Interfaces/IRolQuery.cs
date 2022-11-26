using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IRolQuery
    {
        public Task<List<RolUsiario>> obtenerListaroles();
    }
}
