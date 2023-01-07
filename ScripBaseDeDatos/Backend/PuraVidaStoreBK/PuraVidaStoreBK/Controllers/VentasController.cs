﻿using AutoMapper;
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
    public class VentasController : ControllerBase
    {
        private readonly IVentasQuery _ventas;
        private readonly IMapper _mapper;

        public VentasController(IVentasQuery ventas,IMapper mapper)
        {
            _ventas = ventas;
            _mapper = mapper;
        }
        // GET: api/<VentasController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VentasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VentasController>
        [HttpPost("IngresarVenta"), Authorize(Roles = "1")]
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
