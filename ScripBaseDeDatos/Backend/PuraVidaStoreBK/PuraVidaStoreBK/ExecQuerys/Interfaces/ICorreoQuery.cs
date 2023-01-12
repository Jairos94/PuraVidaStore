using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface ICorreoQuery
    {
        public Task<ParametrosEmail> GuardarEmail(ParametrosEmail email);
        public Task<ParametrosEmail>ObtenerParametroPorId(int id);
    }
}
