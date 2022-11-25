using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
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

        public TipoProducoController(ITipoProductoQuery tipoProductoQuery)
        {
            this._tipoProductoQuery = tipoProductoQuery;
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
            return Ok();
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
