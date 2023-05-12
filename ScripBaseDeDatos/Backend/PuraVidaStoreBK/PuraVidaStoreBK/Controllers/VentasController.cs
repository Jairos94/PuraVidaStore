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
               var retorno =  await _ventas.ingresarFactura(_mapper.Map<Factura>(factura));
                return Ok(_mapper.Map<FacturaDTO>(retorno));
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // PUT api/<VentasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VentasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
