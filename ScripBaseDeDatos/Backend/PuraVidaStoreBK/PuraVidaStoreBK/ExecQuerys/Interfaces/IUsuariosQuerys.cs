using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IUsuariosQuerys
    {
        public object GetUsuario(string Usuario, string Contrasena);
        public  Task<List<Usuario>> ListaUsuarios();
        public Task<Usuario> UsuarioPorId(int id);
        public Task<Usuario> UsuarioIdPersona(int idPersona);
        public Task<Usuario> UsuarioPorUsuario(string usuario);
        public string ocpv(int idUsuario);

        public bool IngresarUsario(Usuario usuario, string clave);
        public bool EditarUsuario(Usuario usuario, string clave);

    }
}
