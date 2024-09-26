using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImpuestoController : ControllerBase
    {
        private readonly IImpuestosQuery _impuestos;
        private readonly IMapper _mapper;

        public ImpuestoController(IImpuestosQuery impuestos,IMapper mapper)
        {
            _impuestos = impuestos;
            _mapper = mapper;
        }

        [HttpGet("ObtenerImpuestoporId"), Authorize]
        public async Task<IActionResult> ObtenerImpuestoporId(int id)
        {
            try
            {
                var resultado =await _impuestos.ObtenerImpuestoPorId(id);
                return Ok( _mapper.Map<ImpuestosDTO>(resultado));
            }
            catch (Exception )
            {

                return BadRequest("Favor de revisar los logs");
            }
        }


        [HttpGet("ObtenerListaImpuestos"),Authorize]
        public async Task<IActionResult> ObtenerListaImpuestos()
        {
            try
            {
                var resultado = await _impuestos.ObtenerListaImpuesto();
                return Ok(_mapper.Map<List< ImpuestosDTO>>(resultado));
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        [HttpGet("ObtenerImpuestosPorDescripcion"),Authorize]
        public async Task<IActionResult> ObtenerImpuestosPorDescripcion(string descripcion) 
        {
            try
            {
                return Ok( _mapper.Map<List<ImpuestosDTO>>(  await _impuestos.ImpuestosPorDescripcion(descripcion)));
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // POST api/<ImpuestoController>
        [HttpPost("GuardarImpuesto"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarImpuesto([FromBody] ImpuestosDTO impuestos)
        {
            try
            {
                var datoGuardar = _mapper.Map<Impuesto>(impuestos);
                var retorno = await _impuestos.GuardarImpuesto(datoGuardar);
                return Ok(_mapper.Map<ImpuestosDTO>(retorno));
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

    }
}
