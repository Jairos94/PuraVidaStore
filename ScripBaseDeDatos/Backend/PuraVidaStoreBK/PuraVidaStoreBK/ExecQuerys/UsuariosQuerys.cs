using PuraVidaStoreBK.ExecQuerys;
using System.Data.SqlClient;
using System.Data;
using PuraVidaStoreBK.Models;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class UsuariosQuerys
    {
        
        DataBase data= new DataBase();
        //Valida Usuario login
        public object GetUsuario(string Usuario, string Contrasena )
        {
            SqlConnection conn=data.GetConnection();
            object Usu = new object();

            try
            {
                
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
                        try
                        {
                            UsuarioModel Usu2= new UsuarioModel();

                            Usu2.IdUsuario = reader.GetInt32(0);
                            Usu2.Usuario = reader.GetString(1);
                            //Usu.password = reader.GetString(2);
                            try
                            {
                                Usu2.email = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                                Usu2.email = "";
                            }

                            Usu2.IdRol = reader.GetInt32(4);
                            Usu2.IdPersona = reader.GetInt32(5);
                            Usu = Usu2;
                        }
                        catch (Exception)
                        {

                           Usu= reader.GetString(0);
                        }
                        

                    

                }
                return (UsuarioModel)Usu;


            }
            catch (Exception ex)
            {
                return Usu;
            }
            finally 
            { conn.Close(); }
        }

        //Ingresa Usuarios
        public bool IngresarUsario(UsuarioModel usuario) 
        {
            SqlConnection conn = data.GetConnection();
            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "IngresarUsuario";
                command.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario.Usuario; ;
                command.Parameters.Add("@Pass", SqlDbType.VarChar, 256).Value = usuario.password;
                command.Parameters.Add("@Email", SqlDbType.VarChar,100).Value = usuario.email;
                command.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;
                command.Parameters.Add("@IdPersona", SqlDbType.Int).Value = usuario.IdPersona;
                reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


            finally
            { conn.Close(); }
        }

        public bool EditarUsuario(UsuarioModel usuario)
        {
            SqlConnection conn = data.GetConnection();
            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "EditarUsuario";
                command.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario.Usuario; ;
                command.Parameters.Add("@Pass", SqlDbType.VarChar, 256).Value = usuario.password;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = usuario.email;
                command.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;
                command.Parameters.Add("@IdPersona", SqlDbType.Int).Value = usuario.IdPersona;
                reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


            finally
            { conn.Close(); }
        }
    }
}
