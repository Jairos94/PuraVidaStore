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
    public class BodegaController : ControllerBase
    {
        private readonly IMapper _maper;
        private readonly IBodegaQuery _bodegaQuery;

        public BodegaController(IMapper maper, IBodegaQuery bodegaQuery)
        {
            _maper = maper;
            _bodegaQuery = bodegaQuery;
        }
        // GET: api/<BodegaController>
        [HttpGet("ListaBodegas")]
        public async Task<IActionResult> ListaBodegas()
        {
            var listaBodegas =_maper.Map<List<BodegaDTO>>(await _bodegaQuery.ListaBodegas()) ;
            return Ok(listaBodegas);
        }

        [HttpGet("ListaBodegasPorDescripcion"), Authorize]
        public async Task<IActionResult> ListaBodegasPorDescripcion(string Descripcion)
        {
            var listaBodegas = _maper.Map<List<BodegaDTO>>(await _bodegaQuery.ListaBodegasPorDescripcion(Descripcion));
            if (listaBodegas != null) 
            {
                return Ok(listaBodegas);
            }
            else 
            {
                return NoContent();
            }
           
        }

        // GET api/<BodegaController>/5
        [HttpGet("BodegaPorId"),Authorize]
        public async Task<IActionResult> BodegaPorId(int id)
        {
            var bodega = _maper.Map<BodegaDTO>(await _bodegaQuery.BodegaPorId(id));
            if (bodega != null) { return Ok(bodega); }
            else { return NoContent(); }
        }

        // POST api/<BodegaController>
        [HttpPost("GuardarBodega"),Authorize(Roles ="1")]
        public async Task<IActionResult> GuardarBodega([FromBody] BodegaDTO bodega)
        {
            var bodegaGuardar =await _bodegaQuery.GuardarBodega(_maper.Map<Bodega>(bodega));

            if (bodegaGuardar != null) 
            { 
                return Ok(_maper.Map<BodegaDTO>(bodegaGuardar));
            }
            else 
            {
                return NoContent();
            }
        }

        
    }
}
