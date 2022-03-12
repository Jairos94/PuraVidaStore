using PuraVidaStoreBK.ExecQuerys;
using System.Data.SqlClient;
using System.Data;
using PuraVidaStoreBK.Models;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class UsuariosQuerys
    {
        DataBase data= new DataBase();
        public UsuarioModel GetUsuario(string Usuario, string Contrasena )
        {
            SqlConnection conn=data.GetConnection();


            try
            {
                UsuarioModel Usu= new UsuarioModel();
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetUsuario";
                command.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = Usuario;
                command.Parameters.Add("@Pass", SqlDbType.VarChar, 50).Value = Contrasena;
                //command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(1) != null)
                    {
                        Usu.IdUsuario = reader.GetInt32(0);
                        Usu.Usuario = reader.GetString(1);
                        Usu.password =  reader.GetString(2);
                        try
                        {
                            Usu.email = reader.GetString(3);
                        }
                        catch (Exception)
                        {

                            Usu.email = "";
                        }
                        
                        Usu.IdRol = reader.GetInt32(4);
                        Usu.IdPersona = reader.GetInt32(5);
                       
                    }

                }
                return Usu;


            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
            finally 
            { conn.Close(); }
        }
    }
}
