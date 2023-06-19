using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class CorreoQuery : ICorreoQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public CorreoQuery(PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<ParametrosEmail> GuardarEmail(ParametrosEmail email)
        {
            try
            {
                    if (email.PreId>0) 
                    {
                    dbContex.ParametrosEmails.Update(email);
                    }
                    else 
                    {
                        dbContex.ParametrosEmails.Add(email);
                    }
                    await dbContex.SaveChangesAsync();
                    return email;
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
                
                    return await dbContex.ParametrosEmails.FindAsync(id);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
