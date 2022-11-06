using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IUsuariosQuerys
    {
        public object GetUsuario(string Usuario, string Contrasena);
        public  Task<List<Usuario>> ListaUsuarios();
    }
}
