using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuariosQuerys Ejecuta = new UsuariosQuerys();

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

        [HttpPost("IngresarUsuario")]
        public async Task<IActionResult> IngresarUsuario(UsuarioModel Usario)
        {

            if (Ejecuta.IngresarUsario(Usario))
            {
                return Ok("Ingreso con exito");
            }
            else
            {
                return BadRequest("Al ingresar");
            }
        }

        [HttpPost("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(UsuarioModel Usario)
        {

            if (Ejecuta.EditarUsuario(Usario))
            {
                return Ok("Ingreso con exito");
            }
            else
            {
                return BadRequest("Al ingresar");
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
