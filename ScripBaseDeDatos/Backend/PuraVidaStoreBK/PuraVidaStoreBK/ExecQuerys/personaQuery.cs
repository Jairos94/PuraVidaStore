using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

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
        public object obtenerPersonaPorCedula(string cedula) 
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext()) 
                {
                    List<Persona> listaP  = new List<Persona>();
                    listaP = db.Personas.Where(x=>x.PsrIdentificacion.Contains(cedula)).ToList();

                    List<PersonaModel> ListaPersonas = new List<PersonaModel>();
                    foreach (Persona persona in listaP) 
                    {
                        PersonaModel model = new PersonaModel();
                        model.PsrId=persona.PsrId;
                        model.PsrIdentificacion=persona.PsrIdentificacion;
                        model.PsrNombre=persona.PsrNombre;
                        model.PsrApellido1=persona.PsrApellido1;
                        model.PsrApellido2=persona.PsrApellido2;
                        ListaPersonas.Add(model);
                    }
                    //var datos = from c in db.Personas where c.PsrIdentificacion.Contains(cedula) select c;
                    return ListaPersonas;
                }

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public object obtenerPersonaPorId(int id)
        {
            try
            {
                using (PuraVidaStoreContext db = new PuraVidaStoreContext())
                {
                    PersonaModel p = new PersonaModel();
                    var persona = db.Personas.Find(id);
                    p.PsrId=persona.PsrId;
                    p.PsrIdentificacion=persona.PsrIdentificacion;
                    p.PsrNombre=persona.PsrNombre;
                    p.PsrApellido1=persona.PsrApellido1;
                    p.PsrApellido2=persona.PsrApellido2;
                    return p;
                }

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public object EditarPersona(Persona persona) 
        {
            using (PuraVidaStoreContext db= new PuraVidaStoreContext()) 
            {
                db.Personas.Update(persona);
                db.SaveChanges();
                return persona;
            }
        }
    }
}
