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

        [HttpGet("Busqueda"), Authorize]
        public async Task<IActionResult> Busqueda(int IdBodega,string Buscador)
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


        [HttpPost("IngresarProductosAlInventario"),Authorize(Roles ="1")]
        public async Task<IActionResult> IngresarProductosAlInventario([FromBody] List<InventariosDTO> InventariosAgregar, int IdBodega, int IdUsuario, int Motivo)
        {
            try
            {
                var seGuardoDatos = await _movimientosQuery.IngresarProductosAlInventario(_mapper.Map<List<Inventarios>>(InventariosAgregar), IdBodega, IdUsuario, Motivo);
                return Ok(seGuardoDatos);
            }
            catch (Exception ex)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }


        [HttpPost("GuardarAjuste"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarAjuste([FromBody] InventariosDTO Inventario, int IdBodega, int IdUsuario, int Motivo)
        {
            try
            {
                var seGuardoDatos = await _movimientosQuery.GuardarAjuste(_mapper.Map<Inventarios>(Inventario), IdBodega, IdUsuario, Motivo);
                return Ok(seGuardoDatos);
            }
            catch (Exception ex)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }

        [HttpPost("GuardarMotivo"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarMotivo([FromBody] MotivosMovimientoDTO motivo)
        {
            try
            {
                var movimientoRetorno =_mapper.Map<MotivosMovimientoDTO>( await _movimientosQuery.GuardarMotivoMovimiento(_mapper.Map<MotivosMovimiento>(motivo)));
                return Ok(movimientoRetorno);
            }
            catch (Exception ex)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }


        [HttpGet("ObtenerMotivos"), Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerMotivos()
        {
            try
            {
                var movimientoRetorno = _mapper.Map<List<MotivosMovimientoDTO>>( await _movimientosQuery.ObtenerListaMotivosMovimiento());
                return Ok(movimientoRetorno);
            }
            catch (Exception )
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }

        [HttpGet("ObtenerMotivosPorDescripcion"), Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerMotivosPorDescripcion(string motivos)
        {
            try
            {
                var movimientoRetorno = _mapper.Map<MotivosMovimientoDTO>( await _movimientosQuery.ObtenerListaMotivoMovimientoPorDescripcion(motivos));
                return Ok(movimientoRetorno);
            }
            catch (Exception)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }

        [HttpGet("ObtenerMotivoPorId"), Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerMotivoPorId(int id)
        {
            try
            {
                var movimientoRetorno =_mapper.Map<MotivosMovimientoDTO>( await _movimientosQuery.ObtenerMotivoMovmientoPorId(id));
                return Ok(movimientoRetorno);
            }
            catch (Exception)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }

        [HttpGet("ObtenerTipoMovimiento"), Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerTipoMovimiento()
        {
            try
            {
               
                return Ok(_mapper.Map<List<TipoMovimientoDTO>>(await _movimientosQuery.ObtenerListaTipoMovimiento()));
            }
            catch (Exception)
            {

                return BadRequest("Se presentó un error favor revisar los logs");
            }
        }


    }
}
