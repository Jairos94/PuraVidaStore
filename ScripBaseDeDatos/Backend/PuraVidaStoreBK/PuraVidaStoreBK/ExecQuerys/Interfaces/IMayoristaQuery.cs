

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IMayoristaQuery
    {
        public Task<ClientesMayorista> buscarClientePorCedulaOId(string buscador);
        public Task<ClientesMayorista> guardarClienteMayorista(ClientesMayorista cliente);
        Task<List<ClientesMayorista>> listaClientesMayorista();
        Task<HistorialClienteMayorista> AgregarAlhistorial(HistorialClienteMayorista historial);
    }
}
