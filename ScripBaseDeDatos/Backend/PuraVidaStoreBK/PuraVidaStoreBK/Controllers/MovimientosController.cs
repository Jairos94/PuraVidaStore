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
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientosQuery _movimientosQuery;
        private readonly IMapper _mapper;

        public MovimientosController(IMovimientosQuery movimientosQuery,IMapper mapper)
        {
            _movimientosQuery = movimientosQuery;
            _mapper = mapper;
        }
        //GET: api/<MovimientosController>
        [HttpGet("Inventarios"), Authorize]
        public async Task<IActionResult> Inventarios(int IdBodega)
        {
            try
            {
                var ListaProductos = await _movimientosQuery.ListaInventarios(IdBodega);
                var retorno = _mapper.Map<List<InventariosDTO>>(ListaProductos);
                return Ok(retorno);
            }
            catch (Exception)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }



        // POST api/<MovimientosController>
        [HttpPost("IngresarPorCompra"), Authorize(Roles = "1")]
        public async Task<IActionResult> IngresarPorCompra(MovimientosDTO movimientos)
        {
            try
            {
                var cambioMovimiento = _mapper.Map<Movimiento>(movimientos);
                var ingresoMovimiento = await _movimientosQuery.IngresoDeProductosPorCompra(cambioMovimiento);
                movimientos = _mapper.Map<MovimientosDTO>(ingresoMovimiento);
                return Ok(movimientos);
            }
            catch (Exception)
            {

                return BadRequest("No se pudo ingresar el movimiento");
            }
            
        }

        

       
    }
}
