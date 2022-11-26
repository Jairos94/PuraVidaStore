using PuraVidaStoreBK.ExecQuerys;
using System.Data.SqlClient;
using System.Data;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class UsuariosQuerys:IUsuariosQuerys
    {
        
        DataBase data= new DataBase();
        //Hace Login
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
                        Usuario u = new Usuario();
                        u.UsrId = reader.GetInt32(0);
                        u.UsrUser = reader.GetString(1);
                        try
                        {
                            u.UsrEmail = reader.GetString(2);
                        }
                        catch (Exception)
                        {

                            u.UsrEmail = "";
                        }
                        u.UsrIdRol = reader.GetInt32(3);
                        u.UsrActivo = reader.GetBoolean(4);
                        u.UsrIdPersona = reader.GetInt32(5);

                        Persona p = new Persona();
                        p.PsrId= u.UsrIdPersona;
                        p.PsrIdentificacion = reader.GetString(6);
                        p.PsrNombre= reader.GetString(7);
                        p.PsrApellido1= reader.GetString(8);
                        p.PsrApellido2=reader.GetString(9);
                        u.UsrIdPersonaNavigation= p;


                        RolUsiario r = new RolUsiario();
                        r.RluId= reader.GetInt32(10);
                        r.RluDescripcion= reader.GetString(11);
                        u.UsrIdRolNavigation= r;
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
                Log.Information("Se presentó un error en Get Usuario\n"+ex);
                return Usu;
            }
            finally
            { conn.Close(); }
        }

        //Obtiene a los usuarios
        public async Task<List<Usuario>> ListaUsuarios()

        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    usuarios = await db.Usuarios
                                       .Where(x=>x.UsrActivo ==true)
                                       .Include(x=>x.UsrIdPersonaNavigation)
                                       .Include(x=>x.UsrIdRolNavigation)
                                       .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Information("Se presentó un error en listaUsuarios\n"+ex);
            }
            return usuarios;
        }

        //Ingresa Usuarios
        public bool IngresarUsario(Usuario usuario,string clave)
        {
            SqlConnection conn = data.GetConnection();
            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "IngresarUsuario";
                command.Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = usuario.UsrUser; ;
                command.Parameters.Add("@Pass", SqlDbType.VarChar, 256).Value = clave;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = usuario.UsrEmail;
                command.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.UsrIdRol;
                command.Parameters.Add("@IdPersona", SqlDbType.Int).Value = usuario.UsrIdPersona;

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

        //edita a un usuario por Id
        public bool EditarUsuario(Usuario usuario,string clave)
        {
            SqlConnection conn = data.GetConnection();
            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "EditarUsuario";
                command.Parameters.Add("@UsrUser", SqlDbType.VarChar, 15).Value = usuario.UsrUser; ;
                command.Parameters.Add("@UsrPass", SqlDbType.VarChar, 256).Value = clave;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = usuario.UsrEmail;
                command.Parameters.Add("@Rol", SqlDbType.Int).Value = usuario.UsrIdRol;
                command.Parameters.Add("@idPersona", SqlDbType.Int).Value = usuario.UsrIdPersona;
                command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuario.UsrId;
                command.Parameters.Add("@activo", SqlDbType.Int).Value = usuario.UsrActivo;

                reader = command.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return false;
            }


            finally
            { conn.Close(); }
        }
        public async Task<Usuario>  UsuarioPorId(int id) 
        {
            var UsuarioRetorno = new Usuario();
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    UsuarioRetorno = await db.Usuarios
                        .Include(x=>x.UsrIdPersonaNavigation)
                        .Include(x=>x.UsrIdRolNavigation)
                        .FirstOrDefaultAsync(x=>x.UsrId == id);
                }
            }
            catch (Exception ex)
            {

                Log.Information("Se presentó un error en UsuarioPorId\n"+ex);
            }
            return UsuarioRetorno;
        }

       

        public async Task< Usuario> UsuarioPorUsuario(string usuario) 
        {
            var usuarioRetorno = new Usuario();
            
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    usuarioRetorno = await db.Usuarios.Where(x => x.UsrUser == usuario).FirstOrDefaultAsync();
                }
            
           
            return usuarioRetorno;
        }

        public async Task<Usuario> UsuarioIdPersona(int idPersona) 
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext())
            {
                return await db.Usuarios.Where(x => x.UsrIdPersona == idPersona).FirstAsync();
            }
        }

   

     

        public string ocpv(int idUsuario) 
        {
            SqlConnection conn = data.GetConnection();
            string dato = "";

            try
            {
                SqlDataReader reader;
                SqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ocpv";
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                //command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    dato = reader.GetString(0);
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dato;

        }
    }
}
