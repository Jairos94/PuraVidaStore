using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DTOS;

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class personaController : ControllerBase
    {
        private readonly IPersonaQuery _persona;
        private readonly IMapper _mapper;

        public personaController(IPersonaQuery persona, IMapper mapper)
        {
            _persona = persona;
           _mapper = mapper;
        }

        // GET api/<personaController>/5
        [HttpGet("obtenerPersonaCedula"), Authorize]
        public async Task<ActionResult> obtenerPersonaCedula(string id)
        {
            var ListaPersonas = await _persona.ObtenerPersonaPorCedula(id);
            var ListaRetorno = _mapper.Map<List<PersonaDto>>(ListaPersonas);


            return Ok(ListaRetorno);
        }

        // GET api/<personaController>/5
        [HttpGet("personaPorId{id}"), Authorize]
        public async Task<ActionResult> personaPorId(long id)
        {
            try
            {
                var PersonaData = await _persona.ObtenerPersonaPorId(id);
                var PersonaRetorno = _mapper.Map<PersonaDto>(PersonaData);
                return Ok(PersonaRetorno);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        [HttpGet("buscarPersonaPorCedula"), Authorize]
        public async Task<ActionResult> buscarPersonaPorCedula(string cedula)
        {
            var dato = await _persona.BuscarUnaPersonaPorCedula(cedula);
            var retorno = _mapper.Map<PersonaDto>(dato);

            return Ok(retorno);
        }
    }
}
