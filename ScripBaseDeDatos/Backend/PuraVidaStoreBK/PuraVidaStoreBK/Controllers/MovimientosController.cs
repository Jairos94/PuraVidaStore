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
        // GET: api/<MovimientosController>
        //[HttpGet("Inventarios"), Authorize(Roles = "1")]
        //public async Task<IActionResult> Inventarios()
        //{
            
        //}

        

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
