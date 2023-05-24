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
    public class MayoristaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMayoristaQuery _mayorista;

        public MayoristaController(IMapper mapper, IMayoristaQuery mayorista)
        {
            _mapper = mapper;
            _mayorista = mayorista;
        }
        // GET: api/<MayoristaController>
        [HttpGet("clienteMayoristaPorCedulaoId"),Authorize]
        public async Task<IActionResult> clienteMayoristaPorCedulaoId(string buscador)
        {
            try
            {
                var retorno = _mapper.Map<ClienteMayoristaDTO>(await _mayorista.buscarClientePorCedulaOId(buscador));
                if (retorno != null)
                {
                    return Ok(retorno);
                }
                else 
                {
                    return NoContent();
                }
               
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // GET api/<MayoristaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MayoristaController>
        [HttpPost("GuardarClienteMayorista"),Authorize]
        public async Task<IActionResult> GuardarClienteMayorista([FromBody] ClienteMayoristaDTO clienteMayorista)
        {
            try
            {
                var guardar = _mapper.Map<ClientesMayorista>(clienteMayorista);
                var retorno =await _mayorista.guardarClienteMayorista(guardar);
                return Ok(_mapper.Map<ClienteMayoristaDTO>( retorno));
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // PUT api/<MayoristaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MayoristaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
