

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IRolesQuerys
    {
        public Task<List<RolUsiario>> obtenerListaroles();
    }
}
