using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class RolesQuerys : IRolesQuerys
    {
        private readonly PuraVidaStoreContext dbContex;

        public RolesQuerys(PuraVidaStoreContext _dbContex )
        {
            dbContex = _dbContex;
        }
        public async Task<List<RolUsiario>> obtenerListaroles()
        {
            var ListaRolUsuarios = new List<RolUsiario>();
                ListaRolUsuarios = await dbContex.RolUsiarios.ToListAsync();
                
            
            return ListaRolUsuarios;
        }
    }
}
