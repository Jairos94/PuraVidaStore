using PuraVidaStoreBK.ExecQuerys;
using System.Data.SqlClient;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class UsuariosQuerys
    {
        DataBase data= new DataBase();
        public void GetUsuario(string Usuario, string Contrasena)
        {
            SqlConnection conn=data.GetConnection();
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "[dbo].[GetUsuario]";
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
