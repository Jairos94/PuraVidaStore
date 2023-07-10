using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentasQuery _ventas;
        private readonly IMapper _mapper;

        public VentasController(IVentasQuery ventas,IMapper mapper)
        {
            _ventas = ventas;
            _mapper = mapper;
        }

        // GET api/<VentasController>/5
        [HttpGet("ObtenerFormasPago"),Authorize]
        public async Task <IActionResult> ObtenerFormasPago()
        {
            
            try
            {
                var retorno = _mapper.Map<List<FormaPagoDTO>>(await _ventas.listaFormaPago());
                return Ok(retorno);
            }
            catch (Exception )
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // POST api/<VentasController>
        [HttpPost("IngresarVenta"), Authorize]
        public async Task<IActionResult> Post([FromBody] FacturaDTO factura )
        {
            try
            {
               var nuevaFactura =  _mapper.Map<Factura>(factura);

                nuevaFactura.FacturaResumen = null;
                nuevaFactura.ImpuestosPorFacturas = null;

                nuevaFactura = await _ventas.ingresarFactura(nuevaFactura);
                var fecha = DateTime.Now;
                nuevaFactura.FtrCodigoFactura = fecha.Year.ToString() + fecha.Month.ToString() + factura.FtrId.ToString();
                await _ventas.actualizarFactura(nuevaFactura);

                if (factura.FacturaResumen!=null) 
                {
                    var nuevaFacturaResumen = new FacturaResumen();
                    foreach (var facturaResumen in factura.FacturaResumen) 
                    {
                        nuevaFacturaResumen = _mapper.Map<FacturaResumen>(facturaResumen);
                    }
                    nuevaFacturaResumen.FtrFactura = nuevaFactura.FtrId;
                    await _ventas.ingresarFacturaResumen(nuevaFacturaResumen);
                }

                if (factura.ImpuestosPorFacturas!=null) 
                {
                    var listaImpuestos = _mapper.Map<List<ImpuestosPorFactura>>(factura.ImpuestosPorFacturas);
                    listaImpuestos = await _ventas.ingresarImpuestosPorFactura(listaImpuestos);
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
                    });
                    await _ventas.ingresarDetalleFactura(listaDetalle);
                }
                var retorno = _mapper.Map<FacturaDTO>(await _ventas.buscarFacturaPorCodigo(nuevaFactura.FtrCodigoFactura));
                return Ok(retorno);
      
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }
    }
}
