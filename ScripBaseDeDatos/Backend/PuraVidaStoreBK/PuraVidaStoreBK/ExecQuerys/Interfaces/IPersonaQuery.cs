

using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.ExecQuerys.Interfaces
{
    public interface IPersonaQuery
    {
        public Task<Persona> IngresarPersona(Persona PersonaData);
        public Task<Persona> EditarPersona(Persona PersonaEditar);
        public Task<List<Persona>> ObtenerPersonaPorCedula(string cedula);
        public  Task<Persona> ObtenerPersonaPorId(int id);
    }
}
