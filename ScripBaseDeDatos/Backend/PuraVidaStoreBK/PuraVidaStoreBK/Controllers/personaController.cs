using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DTOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaQuery _persona;
        private readonly IMapper _mapper;

        public PersonaController(IPersonaQuery persona, IMapper mapper)
        {
            _persona = persona;
            _mapper = mapper;
        }

        // GET api/<personaController>/5
        [HttpGet("obtenerPersonaCedula"),Authorize]
        public async Task<ActionResult> obtenerPersonaCedula(string id)
        {
            var ListaPersonas =await _persona.ObtenerPersonaPorCedula(id);
            var ListaRetorno = _mapper.Map<List<PersonaDto>>(ListaPersonas);


            return Ok( ListaRetorno);
        }

        // GET api/<personaController>/5
        [HttpGet("personaPorId{id}"), Authorize]
        public async Task< ActionResult> personaPorId(int id)
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

    }
}
