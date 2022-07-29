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

        //// GET: api/<personaController>
        //[HttpGet]
        //public ActionResult Get()
        //{

        //}

        // GET api/<personaController>/5
        [HttpGet("obtenerPersonaCedula")]
        public object obtenerPersonaCedula(string id)
        {
            return pq.obtenerPersonaPorCedula(id);
        }

        // GET api/<personaController>/5
        [HttpGet("personaPorId{id}")]
        public object personaPorId(int id)
        {
            return pq.obtenerPersonaPorId(id);
        }

        // POST api/<personaController>
        [HttpPost("agregarPersona")]
        public ActionResult IngresarPersona(PersonaModel persona)
        {

            return Ok(pq.ingresarPersona(persona));
        }

        // PUT api/<personaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<personaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
