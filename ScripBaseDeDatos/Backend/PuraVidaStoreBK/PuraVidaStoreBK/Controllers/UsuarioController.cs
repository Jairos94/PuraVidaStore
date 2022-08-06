using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuariosQuerys Ejecuta = new UsuariosQuerys();
        personaQuery EjecutaPersona = new personaQuery();
        // GET: UsuarioController
        [HttpGet("GetUsuario")]
        public async Task<IActionResult> GetUsuario(string user, string password)
        {
            object Usu = new object();
            Usu = Ejecuta.GetUsuario(user, password);
            try
            {
                return Ok((UsuarioModel)Usu);
            }
            catch (Exception)
            {

                return BadRequest(Usu);
            }

        }
        [HttpPost("GuardarUsario")]
        public ActionResult GuardarUsuario([FromBody] UsuarioModel usuarioM, bool agregar)
        {
            Usuario u = new Usuario();
            Persona p = new Persona();
            PersonaModel pm1 = new PersonaModel();

           pm1.PsrIdentificacion= p.PsrIdentificacion = usuarioM.persona.PsrIdentificacion;
           pm1.PsrNombre=  p.PsrNombre = usuarioM.persona.PsrNombre;
           pm1.PsrApellido1= p.PsrApellido1 = usuarioM.persona.PsrApellido1;
           pm1.PsrApellido2 =p.PsrApellido2 = usuarioM.persona.PsrApellido2;

            //Guarda o actualiza a la persona desde el usuario
            if (usuarioM.IdPersona == 0 ) 
            {
              List<  PersonaModel> p2 = new List<PersonaModel>();
                p2 = (List<PersonaModel>)EjecutaPersona.obtenerPersonaPorCedula(p.PsrIdentificacion);

                //valida la cèdula que no sean usuarios
                if (p2.Count>0) 
                {
                    if (p2[0].PsrIdentificacion == p.PsrIdentificacion && p2 != null)
                    {
                        Usuario u2 = new Usuario();
                        u2 = (Usuario)Ejecuta.UsuarioIdPersona(p.PsrId);
                        if (p.PsrId == u2.UsrIdPersona)
                        {
                            return BadRequest("No se puede guardar el usuario porque otro usuario tiene la misma cédula");
                        }

                    }
                    
                }
                pm1 = (PersonaModel)EjecutaPersona.ingresarPersona(pm1);
                p.PsrId = pm1.PsrId;
            }
            else 
            {
               p= (Persona)EjecutaPersona.EditarPersona(p);
            }
            //Valida si es agregar usuario
            if (agregar) 
            {
                try
                {
                    u = (Usuario)Ejecuta.UsuarioPorUsuario(usuarioM.Usuario);
                }
                catch (Exception)
                {

                    u = null;
                }
                if (u!=null) 
                {
                    if (u.UsrUser == usuarioM.Usuario )
                    {
                        return BadRequest("No se puede guardar a este usuario debido que ya existe");
                    }
                }
                usuarioM.IdPersona = p.PsrId;
                Ejecuta.IngresarUsario(usuarioM);
            }
            //Valida se es editar
            else 
            {
                UsuarioModel um = new UsuarioModel();
                PersonaModel pm = new PersonaModel();
                
                um = (UsuarioModel)Ejecuta.UsuarioPorId((int)usuarioM.IdUsuario);

                //valida si el usuario cambio existe
                if (usuarioM.IdUsuario == um.IdUsuario && usuarioM.Usuario!=um.Usuario) 
                {
                    u = (Usuario)Ejecuta.UsuarioPorUsuario(usuarioM.Usuario);
                    if (u.UsrUser == usuarioM.Usuario) 
                    {
                        return BadRequest("No se puede guardar a este usuario debido que ya existe");
                    }
                }
                Ejecuta.EditarUsuario(usuarioM);

            }

            
            
            return Ok(true);
        }

        [HttpGet("ListaUsuarios")]
        public async Task<IActionResult> ListaUsuarios()
        //public ActionResult  GetUsuario()
        {
            object Usu = new object();
            Usu = Ejecuta.listaUsuarios();
            try
            {
                return Ok(Usu);
            }
            catch (Exception)
            {

                return BadRequest(Usu);
            }

        }

      

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("UsuarioPorId")]
        public ActionResult UsuarioPorId(int id)
        {
            try
            {
                return Ok(Ejecuta.UsuarioPorId(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
