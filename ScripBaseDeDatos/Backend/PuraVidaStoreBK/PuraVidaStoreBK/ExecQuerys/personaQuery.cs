using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;
using Serilog;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class PersonaQuery:IPersonaQuery
    {
        public async Task<Persona>  IngresarPersona(Persona PersonaData) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                   
                    await db.Personas.AddAsync(PersonaData);
                    db.SaveChanges();
                }
                
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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    ListaPersonas =  db.Personas
                        .Where(x=>x.PsrIdentificacion.Contains(cedula))
                        .ToList();
                }

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
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    PersonaRetorno = await db.Personas.FindAsync(id);
                }

            }
            catch (Exception ex)
            {
                Log.Information("Se prersentó un error en ObtenerPersonaPorId\n"+ex);
                
            }
            return PersonaRetorno;
        }

        public async Task<Persona> EditarPersona(Persona PersonaEditar) 
        {
            using (PuraVidaStoreContext db= new PuraVidaStoreContext()) 
            {
                db.Personas.Update(PersonaEditar);
                db.SaveChanges();
                
            }

            return PersonaEditar;
        }
    }
}
