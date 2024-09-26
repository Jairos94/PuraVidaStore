

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IPersonaQuery
    {
        Task<Persona> IngresarPersona(Persona PersonaData);
        Task<Persona> EditarPersona(Persona PersonaEditar);
        Task<List<Persona>> ObtenerPersonaPorCedula(string cedula);
         Task<Persona> ObtenerPersonaPorId(long id);
        Task<Persona> BuscarUnaPersonaPorCedula(string cedula);
    }
}
