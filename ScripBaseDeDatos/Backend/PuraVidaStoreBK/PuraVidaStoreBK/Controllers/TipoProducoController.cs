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
       
        
        // GET: api/<TipoProducoController>
        [HttpPost("GuardarTipoProducto"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarTipoProducto(TipoProductoDTO tipoProducto)
        {
            var tipoProductoGuardar = _mapper.Map<TipoMovimientos>(tipoProducto);
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
        [HttpGet("ObtenerTipoProductoPorId"), Authorize]
        public async Task<IActionResult> ObtenerTipoProductoPorId(int id)
        {
            var Producto = await _tipoProductoQuery.ProductoPorId(id);
            var tipoProducto = _mapper.Map<TipoProductoDTO>(Producto);
            if (tipoProducto != null) 
            {
                return Ok(tipoProducto);
            }
            else 
            {
                return NoContent();
            }
        }

        [HttpGet("ListaTipoProducto"),Authorize]
        public async Task<IActionResult> ListaTipoProducto() 
        {
            var listaProducto = await _tipoProductoQuery.ListaTipoProducto();
            var lIstProductoRetorno = _mapper.Map<List<TipoProductoDTO>>(listaProducto);
            return Ok(lIstProductoRetorno);
        }

        [HttpGet("ListaTipoProductoFiltrado"),Authorize]
        public async Task<IActionResult> ListaTipoProductoFiltrado() 
        {
            var listaProducto = await _tipoProductoQuery.ListaProductoFiltrado();
            var lIstProductoRetorno = _mapper.Map<List<TipoProductoDTO>>(listaProducto);
            return Ok(lIstProductoRetorno);
        }

        [HttpGet("BuscarTipoProductoPorDescripcion"),Authorize]
        public async Task<IActionResult> BuscarTipoProductoPorDescripcion(string Descripcion) 
        {
            var listaProducto = await _tipoProductoQuery.BuscarTipoProductoPorDescripcion(Descripcion);
            var lIstProductoRetorno = _mapper.Map<List<TipoProductoDTO>>(listaProducto);
            return Ok(lIstProductoRetorno);
        }
    }
}
