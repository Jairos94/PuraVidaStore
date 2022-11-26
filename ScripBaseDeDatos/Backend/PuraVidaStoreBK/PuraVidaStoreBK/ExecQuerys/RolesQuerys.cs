using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class RolesQuerys : IRolQuery
    {

        public async Task<List<RolUsiario>> obtenerListaroles()
        {
            var ListaRolUsuarios = new List<RolUsiario>();
            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
                ListaRolUsuarios = await db.RolUsiarios.ToListAsync();
                
            }
            return ListaRolUsuarios;
        }
    }
}
