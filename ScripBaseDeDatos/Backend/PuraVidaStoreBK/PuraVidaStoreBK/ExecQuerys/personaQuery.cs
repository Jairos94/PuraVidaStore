using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex2;

namespace PuraVidaStoreBK.ExecQuerys
{
    public class personaQuery
    {
        public object ingresarPersona(PersonaModel p) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    Persona persona = new Persona();
                    persona.PsrNombre = p.PsrNombre;
                    persona.PsrIdentificacion=p.PsrIdentificacion;
                    persona.PsrApellido1 = p.PsrApellido1;
                    persona.PsrApellido2=p.PsrApellido2;
                    db.Personas.Add(persona);
                    db.SaveChanges();

                    p.PsrId = persona.PsrId;
                        
                    return p;
                }
                
            }
            catch (Exception ex)
            {

               return ex.Message;
            }
            
        }
    }
}
