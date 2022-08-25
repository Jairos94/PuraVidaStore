using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class personaController : ControllerBase
    {
        personaQuery pq = new personaQuery();

        // GET api/<personaController>/5
        [HttpGet("obtenerPersonaCedula"),Authorize]
        public object obtenerPersonaCedula(string id)
        {
            return pq.obtenerPersonaPorCedula(id);
        }

        // GET api/<personaController>/5
        [HttpGet("personaPorId{id}"), Authorize]
        public ActionResult personaPorId(int id)
        {
            try
            {
                return Ok( pq.obtenerPersonaPorId(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        // POST api/<personaController>
        [HttpPost("agregarPersona"), Authorize]
        public ActionResult IngresarPersona(PersonaModel persona)
        {

            return Ok(pq.ingresarPersona(persona));
        }
    }
}
