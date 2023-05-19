using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly IParametrosGeneralesQuery _parametros;
        private readonly IMapper _mapper;

        public ParametrosController(IParametrosGeneralesQuery parametros, IMapper mapper)
        {
            _parametros = parametros;
            _mapper = mapper;
        }

        [HttpPost("GuardarParametrosGlobales"), Authorize]
        public async Task<IActionResult> GuardarParametrosGlobales(ParametrosGlobalesDTO parametros)
        {
            try
            {
                var guardar = await _parametros.GuardarParametros(_mapper.Map<ParametrosGlobales>(parametros));
                
                
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // GET api/<ParametrosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ParametrosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ParametrosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ParametrosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
