using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using Serilog;
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
        private readonly IProductoQuery _productoQuery;
        private readonly IBodegaQuery _bodegaQuery;

        public MovimientosController(IMovimientosQuery movimientosQuery,IMapper mapper, IProductoQuery productoQuery,IBodegaQuery bodega)
        {
            _movimientosQuery = movimientosQuery;
            _mapper = mapper;
            _productoQuery = productoQuery;
            _bodegaQuery = bodega;
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
                var datosTrasformados = _mapper.Map<List<Inventarios>>(InventariosAgregar);
                foreach (var dato in datosTrasformados) 
                {
                    dato.producto.PdrVisible = true;
                    dato.producto.PdrTieneExistencias = true;


                    await _productoQuery.GuardarProducto(dato.producto, IdUsuario);
                }
                var seGuardoDatos = await _movimientosQuery.IngresarProductosAlInventario(datosTrasformados, IdBodega, IdUsuario, Motivo);
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
                var movimientoRetorno = _mapper.Map<List< MotivosMovimientoDTO>>( await _movimientosQuery.ObtenerListaMotivoMovimientoPorDescripcion(motivos));
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

        [HttpPost("GuardarTraslado"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarTraslado([FromBody] TrasladoEntreBodegasDTO traslado) 
        {
            try
            {
                var bodegaOrigen = await _bodegaQuery.BodegaPorId(traslado.idBodegaOrigen);
                var BodegaDestino = await _bodegaQuery.BodegaPorId(traslado.idBodegaDestino);

                var descricionBodega = string.Format("Traslado entre bodegas [origen:{0},destino:{1}]", bodegaOrigen.BdgDescripcion, BodegaDestino.BdgDescripcion);
                var lista = new List<MotivosMovimiento>();
                lista = await _movimientosQuery.BusquedaPorDescripcion(descricionBodega);
                if (lista.Count == 0)
                {
                    lista.Add(new MotivosMovimiento
                    {
                        MtmIdTipoMovimiento = 1,
                        MtmDescripcion = descricionBodega
                    });
                    lista.Add(new MotivosMovimiento
                    {
                        MtmIdTipoMovimiento = 2,
                        MtmDescripcion = descricionBodega
                    });
                    lista = await _movimientosQuery.GuardarListaMotivos(lista);
                }
                var listaMovimientos = new List<Movimiento>();
                var fecha = DateTime.Now;

                traslado.productos.ForEach(x => 
                {
                    var salidaBodegaOrigen = new Movimiento
                    {
                        MvmIdProducto = x.idProducto,
                        MvmCantidad = x.cantidad,
                        MvmFecha = fecha,
                        MvmIdMotivoMovimiento = lista[1].MtmId,
                        MvmIdUsuario = traslado.idUsuario,
                        MvmIdBodega = traslado.idBodegaOrigen

                    };
                    listaMovimientos.Add(salidaBodegaOrigen);

                    var entradaBodegaDestino = new Movimiento
                    {
                        MvmIdProducto = x.idProducto,
                        MvmCantidad = x.cantidad,
                        MvmFecha = fecha,
                        MvmIdMotivoMovimiento = lista[0].MtmId,
                        MvmIdUsuario = traslado.idUsuario,
                        MvmIdBodega = traslado.idBodegaDestino

                    };
                    listaMovimientos.Add(entradaBodegaDestino);
                });
               
                var retorno = _mapper.Map<List<MovimientosDTO>>(await _movimientosQuery.GuardarListaMovimientos(listaMovimientos));
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                return BadRequest("Favor de revisar los logs");
            }
           
        }

    }
}
