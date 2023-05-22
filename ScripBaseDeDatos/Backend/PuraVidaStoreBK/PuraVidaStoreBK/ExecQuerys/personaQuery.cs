
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class PersonaQuery:IPersonaQuery
    {
        private readonly PuraVidaStoreContext dbContex;

        public PersonaQuery( PuraVidaStoreContext _dbContex)
        {
            dbContex = _dbContex;
        }
        public async Task<Persona>  IngresarPersona(Persona PersonaData) 
        {
            try
            {
                    await dbContex.Personas.AddAsync(PersonaData);
                    dbContex.SaveChanges();
                
                
            }
            catch (Exception ex)
            {

                Log.Information("Se presentó un error en IngresarPersona\n"+ex);
            }
            return PersonaData;

        }
        public async Task<List<Persona>> ObtenerPersonaPorCedula(string cedula) 
        {
            var ListaPersonas = new List<Persona>();
            try
            {
                
                    ListaPersonas = dbContex.Personas
                        .Where(x=>x.PsrIdentificacion.Contains(cedula))
                        .ToList();
                

            }
            catch (Exception ex)
            {

                Log.Information("Se present[o un error en ObtenerPersonaPorCedula\n"+ex);
            }
            return ListaPersonas;
        }

        public async Task<Persona> ObtenerPersonaPorId(int id)
        {
            var PersonaRetorno = new Persona();
            try
            {
              
                    PersonaRetorno = await dbContex.Personas.FindAsync(id);
                

            }
            catch (Exception ex)
            {
                Log.Information("Se prersentó un error en ObtenerPersonaPorId\n"+ex);
                
            }
            return PersonaRetorno;
        }

        public async Task<Persona> EditarPersona(Persona PersonaEditar) 
        {
            try
            {
                dbContex.Update(PersonaEditar);
                await dbContex.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }
             
                
            

            return PersonaEditar;
        }
    }
}
