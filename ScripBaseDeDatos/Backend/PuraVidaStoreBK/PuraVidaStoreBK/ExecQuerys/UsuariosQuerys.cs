using PuraVidaStoreBK.ExecQuerys;
using System.Data.SqlClient;
using System.Data;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class UsuariosQuerys
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
                        UsuarioModel u = new UsuarioModel();
                        u.IdUsuario = reader.GetInt32(0);
                        u.Usuario = reader.GetString(1);
                        try
                        {
                            u.email = reader.GetString(2);
                        }
                        catch (Exception)
                        {

                            u.email = "";
                        }
                        u.IdRol = reader.GetInt32(3);
                        u.IdPersona = reader.GetInt32(4);

                        PersonaModel p = new PersonaModel();
                        p.PsrId= u.IdPersona;
                        p.PsrIdentificacion = reader.GetString(5);
                        p.PsrNombre= reader.GetString(6);
                        p.PsrApellido1= reader.GetString(7);
                        p.PsrApellido2=reader.GetString(8);
                        u.persona= p;


                        RolModel r = new RolModel();
                        r.RluID= reader.GetInt32(9);
                        r.RluDescripcion= reader.GetString(10);
                        u.Rol= r;
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

        //Obtiene a los usuarios
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

        //edita a un usuario por Id
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
                command.Parameters.Add("@UsrUser", SqlDbType.VarChar, 15).Value = usuario.Usuario; ;
                command.Parameters.Add("@UsrPass", SqlDbType.VarChar, 256).Value = usuario.password;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = usuario.email;
                command.Parameters.Add("@Rol", SqlDbType.Int).Value = usuario.IdRol;
                command.Parameters.Add("@idPersona", SqlDbType.Int).Value = usuario.IdPersona;
                command.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuario.IdUsuario;
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
        public object UsuarioPorId(int id) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                   
                    var u= db.Usuarios.Find(id);
                    var r=db.RolUsiarios.Find(u.UsrIdRol);
                    var p = db.Personas.Find(u.UsrIdPersona);

                    //carga models
                    UsuarioModel um = new UsuarioModel();
                  
                    um.IdUsuario = u.UsrId;
                    um.Usuario = u.UsrUser;
                    um.password = "";
                    um.email=u.UsrEmail;
                    um.IdRol=u.UsrIdRol;
                    um.IdPersona = u.UsrIdPersona;

                    PersonaModel pm = new PersonaModel();
                    pm.PsrId = p.PsrId;
                    pm.PsrIdentificacion = p.PsrIdentificacion;
                    pm.PsrNombre = p.PsrNombre;
                    pm.PsrApellido1 = p.PsrApellido1;
                    pm.PsrApellido2 = p.PsrApellido2;
                    um.persona = pm;
                    RolModel rm = new RolModel();
                    rm.RluID = r.RluId;
                    rm.RluDescripcion = r.RluDescripcion;
                    um.Rol = rm;
                    return um;
                    
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        //devuelve el password
        public object UsuarioPorId2(int id)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {

                    return db.Usuarios.Find(id).UsrPass;

                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public object UsuarioPorUsuario(string usuario) 
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
            {
                return db.Usuarios.Where(x=>x.UsrUser == usuario);
            }
        }

        public object UsuarioIdPersona(int idPersona) 
        {
            using (PuraVidaStoreContext db = new PuraVidaStoreContext())
            {
                return db.Usuarios.Where(x => x.UsrIdPersona == idPersona).First();
            }
        }

        //elimina al usuario
        public object EliminarUsuario(int idUsuario) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    var usuario = db.Usuarios.Find(idUsuario);
                    db.Usuarios.Remove(usuario);
                    db.SaveChanges();

                    var usuMo = new UsuarioModel 
                    {
                        IdUsuario= usuario.UsrId,
                        Usuario = usuario.UsrUser,


                    };
                    return usuMo;
                }
               
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }
    }
}
