using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class RolesQuerys
    {
        public List<RolModel> listaRoles() 
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
                List<RolUsiario> roles = new  List<RolUsiario>();
                roles=db.RolUsiarios.ToList();

                List<RolModel> listaRoles = new List<RolModel>();
                foreach (RolUsiario r in roles) 
                {
                    RolModel rm = new RolModel() 
                    {
                        RluID=r.RluId,
                        RluDescripcion=r.RluDescripcion
                    };
                    listaRoles.Add(rm);
                }
                return listaRoles;
            }
        }
    }
}
