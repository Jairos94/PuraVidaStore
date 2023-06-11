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
    public class ProductosController : ControllerBase
    {
        private readonly IProductoQuery _productoQuery;
        private readonly IMapper _mapper;

        public ProductosController(IProductoQuery productoQuery, IMapper mapper)
        {
            _productoQuery = productoQuery;
            _mapper = mapper;
        }
        [HttpGet("ListaProductos"), Authorize]
        public async Task<IActionResult> ListaProductos()
        {

            var listaProductos = await _productoQuery.ListaProductosFiltrada();
            var listaRetorno = _mapper.Map<List<ProductoDTO>>(listaProductos);
            return Ok(listaRetorno);
        }

        [HttpGet("ObtenerTodosLosProductos"), Authorize]
        public async Task<IActionResult> ObtenerTodosLosProductos()
        {

            var listaProductos = await _productoQuery.ListaProductos();
            var listaRetorno = _mapper.Map<List<ProductoDTO>>(listaProductos);
            return Ok(listaRetorno);
        }

        [HttpGet("ObtenerTodosLosProductosNoFiltrada"), Authorize]
        public async Task<IActionResult> ObtenerTodosLosProductosNoFiltrada()
        {

            var listaProductos = await _productoQuery.ListaProductosNoFiltrada();
            var listaRetorno = _mapper.Map<List<ProductoDTO>>(listaProductos);
            return Ok(listaRetorno);
        }


        [HttpGet("ListaProductosPorDescripcion"), Authorize]
        public async Task<IActionResult> ListaProductosPorDescripcion(string Descripcion)
        {

            var listaProductos = await _productoQuery.ProductoPorDescripcion(Descripcion);
            var listaRetorno = _mapper.Map<List<ProductoDTO>>(listaProductos);
            return Ok(listaRetorno);
        }
        [HttpGet("ListaProductosPorDescripcionNoFiltrada"), Authorize]
        public async Task<IActionResult> ListaProductosPorDescripcionNoFiltrada(string Descripcion)
        {

            var listaProductos = await _productoQuery.ProductoPorNoFiltradaDescripcion(Descripcion);
            var listaRetorno = _mapper.Map<List<ProductoDTO>>(listaProductos);
            return Ok(listaRetorno);
        }
        [HttpGet("ObtenerProductoPorId"), Authorize]
        public async Task<IActionResult> ObtenerProductoPorId(long id)
        {
            var producto = await _productoQuery.ProductoPorId(id);
            var productoRenorno = _mapper.Map<ProductoDTO>(producto);

            if (productoRenorno != null)
            {
                return Ok(productoRenorno);
            }
            else { return NoContent(); }
        }

        [HttpGet("BusquedaPorCodigo"), Authorize]
        public async Task<IActionResult> BusquedaPorCodigo(string codigo) 
        {
            var resultado = await _productoQuery.BuscarProductoPorCodigo(codigo);
            if (resultado.PrdId > 0)
            {
                return Ok(_mapper.Map<ProductoDTO>(resultado));
            }
            else
            {
                return BadRequest();
            }

          
        }

        [HttpPost("GuardarProducto"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarProducto([FromBody] ProductoDTO model,int idUsuario)
        {
            var productoGuardar = _mapper.Map<Producto>(model);
            productoGuardar = await _productoQuery.GuardarProducto(productoGuardar, idUsuario);
            model = _mapper.Map<ProductoDTO>(productoGuardar);
            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("Se presentó un error a la hora de guardar el _producto");
            }
        }
    }
}
