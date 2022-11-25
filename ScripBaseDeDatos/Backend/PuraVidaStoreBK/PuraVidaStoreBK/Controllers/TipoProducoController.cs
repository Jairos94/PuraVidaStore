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
    public class TipoProducoController : ControllerBase
    {
        private readonly ITipoProductoQuery _tipoProductoQuery;
        private readonly IMapper _mapper;

        public TipoProducoController(ITipoProductoQuery tipoProductoQuery,IMapper mapper)
        {
            _tipoProductoQuery = tipoProductoQuery;
            _mapper = mapper;
        }
        //[HttpPost("GuardarTipoProducto"), Authorize(Roles = "1")]
        //[HttpGet("ListaTipoProducto"),Authorize]
        // [HttpGet("ListaTipoProductoFiltrado"),Authorize]
        // [HttpGet("ObtenerTipoProductoPorId"), Authorize(Roles = "1")]
        //[HttpGet("BuscarTipoProductoPorDescripcion"),Authorize]

        // GET: api/<TipoProducoController>
        [HttpPost("GuardarTipoProducto"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarTipoProducto(TipoProductoDTO tipoProducto)
        {
            var tipoProductoGuardar = _mapper.Map<TipoProducto>(tipoProducto);
            tipoProductoGuardar =await _tipoProductoQuery.Guardar(tipoProductoGuardar);
            tipoProducto = _mapper.Map<TipoProductoDTO>(tipoProductoGuardar);
            if (tipoProducto!=null) 
            {
                return Ok(tipoProducto);
            }
            else 
            {
                return BadRequest(tipoProducto);
            }
            
        }

        // GET api/<TipoProducoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TipoProducoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TipoProducoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TipoProducoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
