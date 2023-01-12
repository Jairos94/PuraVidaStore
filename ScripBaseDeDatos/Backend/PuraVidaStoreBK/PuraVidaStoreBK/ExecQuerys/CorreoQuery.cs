using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class CorreoQuery : ICorreoQuery
    {
        public async Task<ParametrosEmail> GuardarEmail(ParametrosEmail email)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    if (email.PreId>0) 
                    {
                        db.ParametrosEmails.Update(email);
                    }
                    else 
                    {
                        db.ParametrosEmails.Add(email);
                    }
                    await db.SaveChangesAsync();
                    return email;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<ParametrosEmail> ObtenerParametroPorId(int id)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    return await db.ParametrosEmails.FindAsync(id);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
