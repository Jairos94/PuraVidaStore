using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using XAct;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentasQuery _ventas;
        private readonly IMapper _mapper;
        private readonly IMayoristaQuery _mayorista;

        public VentasController(IVentasQuery ventas, IMapper mapper, IMayoristaQuery mayorista)
        {
            _ventas = ventas;
            _mapper = mapper;
            _mayorista = mayorista;
        }

        // GET api/<VentasController>/5
        [HttpGet("ObtenerFormasPago"), Authorize]
        public async Task<IActionResult> ObtenerFormasPago()
        {

            try
            {
                var retorno = _mapper.Map<List<FormaPagoDTO>>(await _ventas.listaFormaPago());
                return Ok(retorno);
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // POST api/<VentasController>
        [HttpPost("IngresarVenta"), Authorize]
        public async Task<IActionResult> IngresarVenta([FromBody] FacturaDTO factura)
        {
            try
            {
                var nuevaFactura = _mapper.Map<Factura>(factura);

                nuevaFactura.FacturaResumen = null;
                nuevaFactura.ImpuestosPorFacturas = null;
                nuevaFactura.DetalleFacturas = null;
                var fecha = DateTime.Now;
                nuevaFactura.FtrFecha = fecha;
                nuevaFactura = await _ventas.ingresarFactura(nuevaFactura);

                nuevaFactura.FtrCodigoFactura = fecha.Year.ToString() + fecha.Month.ToString() + nuevaFactura.FtrId.ToString();
                await _ventas.actualizarFactura(nuevaFactura);

                if (factura.FacturaResumen != null)
                {
                    var nuevaFacturaResumen = new FacturaResumen();
                    foreach (var facturaResumen in factura.FacturaResumen)
                    {
                        nuevaFacturaResumen = _mapper.Map<FacturaResumen>(facturaResumen);
                    }
                    nuevaFacturaResumen.FtrFactura = nuevaFactura.FtrId;
                    await _ventas.ingresarFacturaResumen(nuevaFacturaResumen);
                }

                if (factura.DetalleFacturas != null)
                {
                    var listaDetalle = _mapper.Map<List<DetalleFactura>>(factura.DetalleFacturas);
                    var contador = 0;
                    listaDetalle.ForEach(x =>
                    {
                        contador++;
                        x.DtfIdFactura = nuevaFactura.FtrId;
                        x.DtfLinea = contador;
                        x.DtfIdProducto1 = null;
                        x.DtfIdProductoNavigation = null;

                    });
                    await _ventas.ingresarDetalleFactura(listaDetalle);
                }

                if (factura.ImpuestosPorFacturas != null)
                {
                    var listaImpuestos = _mapper.Map<List<ImpuestosPorFactura>>(factura.ImpuestosPorFacturas);
                    var contadorImpuestos = 0;
                    listaImpuestos.ForEach(x =>
                    {
                        contadorImpuestos++;
                        x.IpfIdImpuesto = contadorImpuestos;

                        x.IpfIdFactura = nuevaFactura.FtrId;
                    });
                    listaImpuestos = await _ventas.ingresarImpuestosPorFactura(listaImpuestos);
                }
            

                var consulta = await _ventas.buscarFacturaPorCodigo(nuevaFactura.FtrCodigoFactura);
                var retorno = _mapper.Map<FacturaDTO>(consulta);
                return Ok(retorno);

            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        [HttpGet("ObtenerFacturasDelMes"), Authorize]
        public async Task<IActionResult> ObtenerFacturasDelMes(int IdBodega) 
        {
            try
            { 
                var facturas =await _ventas.facturasMes(IdBodega);
                var retorno = new List<EstructuraFacturaDTO>();
                foreach (var factura in facturas) 
                {
                    var resumen = new FacturaResumen();
                    foreach (var datoResumen in factura.FacturaResumen) 
                    {
                        resumen = datoResumen;
                    };

                    var dato = new EstructuraFacturaDTO
                    {
                        NumeroFctura = factura.FtrCodigoFactura,
                        Fecha = factura.FtrFecha,
                        EsFacturaNula = (bool)factura.FtrEsFacturaNula,
                        EstadoFactura = factura.FtrEsFacturaNula == true ? "Factura Nula" : "Factura Activa",
                        Total = resumen.FtrMontoTotal
                    };
                    retorno.Add(dato);
                }
                retorno = retorno.OrderByDescending(x => x.Fecha).ToList();
                return Ok(retorno);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("ObtenerFacturaPorCodigo"), Authorize]
        public async Task<IActionResult> ObtenerFacturaPorCodigo(string codigo) 
        {
            try
            {
                var factura = await _ventas.consultarFactura(codigo);
                factura.DetalleFacturas = await _ventas.consultarDetallePorFactura(factura.FtrId);
                factura.DetalleFacturas.ForEach(x=> 
                {
                    x.DtfIdProducto1.PdrFoto = null;
                    x.DtfIdProductoNavigation = null;
                });
                return Ok(_mapper.Map<FacturaDTO>(factura));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("AnularFactura"), Authorize]
        public async Task<IActionResult> AnularFactura([FromBody] IngresarHistorialDTO ingreso) 
        {
            try
            {
                var factura = await _ventas.consultarFactura(ingreso.IdFactura.ToString());
                factura.FtrEsFacturaNula = true;
                await _ventas.actualizarFactura(factura);
                var historial = new HistorialFacturasAnulada 
                {
                    HlfId=0,
                    HlfIdUsuario=ingreso.idUsuario,
                    HlfIdFctura= ingreso.IdFactura,
                    HlfRazon = ingreso.Descripcion
                };
               var retorno= await _ventas.ingresarHistorialNulas(_mapper.Map<HistorialFacturasAnulada>( historial));
                return Ok(retorno);
                
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
