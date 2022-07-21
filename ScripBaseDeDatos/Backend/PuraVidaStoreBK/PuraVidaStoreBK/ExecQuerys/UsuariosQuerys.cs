using PuraVidaStoreBK.ExecQuerys;
using System.Data.SqlClient;
using System.Data;
using PuraVidaStoreBK.Models;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class UsuariosQuerys
    {
        
        DataBase data= new DataBase();
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
                        UsuarioModel u = new UsuarioModel();
                        u.IdUsuario = reader.GetInt32(0);
                        u.Usuario = reader.GetString(1);
                        u.email=reader.GetString(3);
                        u.IdRol = reader.GetInt32(4);
                        u.IdPersona = reader.GetInt32(5);
                       Usu = u;
                    }
                    catch (Exception)
                    {

                        Usu = reader.GetString(0);
                    }
                }
                return Usu;
            }

            catch (Exception ex)
            {
                return Usu;
            }
            finally
            { conn.Close(); }
        }

        public object listaUsuarios()

        {
            SqlConnection conn = data.GetConnection();
            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ObtenerUsuarios";
                //command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                List<UsuarioModel> ListaUsuarios= new List<UsuarioModel>();
          
                while (reader.Read())
                {
                    UsuarioModel u = new UsuarioModel();
                    RolModel r = new RolModel();
                    PersonaModel p = new PersonaModel();

                    u.IdUsuario = reader.GetInt32(0);
                        u.Usuario = reader.GetString(1);
                    try
                    {
                        u.email = reader.GetString(2);
                    }
                    catch (Exception)
                    {

                        u.email ="";
                    }
                        
                        u.IdRol = reader.GetInt32(3);
                        u.IdPersona = reader.GetInt32(4);

                        
                        r.RluID = reader.GetInt32(5);
                        r.RluDescripcion = reader.GetString(6);
                        u.Rol = r;

                        
                        p.PsrId = reader.GetInt32(7);
                        p.PsrIdentificacion= reader.GetString(8);
                        p.PsrNombre = reader.GetString(9);
                        p.PsrApellido1= reader.GetString(10);
                        p.PsrApellido2= reader.GetString(11);
                        u.persona = p;
                        ListaUsuarios.Add(u); 
                }
                return ListaUsuarios;
            }

            catch (Exception ex)
            {
                return ex.Message;
               
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
