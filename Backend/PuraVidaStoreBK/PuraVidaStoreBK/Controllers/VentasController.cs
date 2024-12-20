﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using PuraVidaStoreBK.Utilitarios.Interfase;
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
        private readonly IParametrosGeneralesQuery _parametros;
        private readonly IEnvioCorreo _envioCorreo;
		private readonly IImprimirTiquete _imprimir;

		public VentasController(IVentasQuery ventas, IMapper mapper, IMayoristaQuery mayorista, IParametrosGeneralesQuery parametros, IEnvioCorreo envioCorreo,IImprimirTiquete imprimir)
        {
            _ventas = ventas;
            _mapper = mapper;
            _mayorista = mayorista;
            _parametros = parametros;
            _envioCorreo = envioCorreo;
			_imprimir = imprimir;
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
                var facturaGuardado = _ventas.GuardarFactura(nuevaFactura);

                var consulta = await _ventas.buscarFacturaPorCodigo(nuevaFactura.FtrCodigoFactura);
                var retorno = _mapper.Map<FacturaDTO>(consulta);


                var parametrosGlobales = await _parametros.ObtenerParametrosId(retorno.FtrBodega);
                if (parametrosGlobales != null /*&& parametrosGlobales.ParametrosEmail != null*/)
                {
                    var facturaEmail = await _ventas.consultarFactura(retorno.FtrCodigoFactura);
                    facturaEmail.DetalleFacturas = await _ventas.ConsultarDetallePorFactura(facturaEmail.FtrId);
                    var facturaFaltantes = await _ventas.ConsultarFormaPagoPorFactura(facturaEmail.FtrId);
                    facturaEmail.FtrFormaPagoNavigation = facturaFaltantes.FtrFormaPagoNavigation;
                    facturaEmail.FtrBodegaNavigation = facturaFaltantes.FtrBodegaNavigation;
                    var listaCorreo = new List<string> { facturaEmail.FtrCorreoEnvio };
                   // _envioCorreo.EnviarFactura(facturaEmail, parametrosGlobales.ParametrosEmail, listaCorreo);
                    _imprimir.Imprimir(facturaEmail, parametrosGlobales,false);
                }



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
                var facturas = await _ventas.facturasMes(IdBodega);
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
                factura.DetalleFacturas = await _ventas.ConsultarDetallePorFactura(factura.FtrId);
                factura.DetalleFacturas.ForEach(x =>
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

        [HttpPost("FacturaNula"), Authorize]
        public async Task<IActionResult> AnularFactura([FromBody] HistorialFacturasAnuladaDTO ingreso)
        {
            try
            {
                var factura = await _ventas.consultarFactura(ingreso.HlfIdFctura.ToString());
                factura.FtrEsFacturaNula = true;
                await _ventas.actualizarFactura(factura);
                var historial = new HistorialFacturasAnulada
                {
                    HlfId = 0,
                    HlfIdUsuario = ingreso.HlfIdUsuario,
                    HlfIdFctura = ingreso.HlfIdFctura,
                    HlfRazon = ingreso.HlfRazon
                };
                var retorno = await _ventas.ingresarHistorialNulas(_mapper.Map<HistorialFacturasAnulada>(historial));
                return Ok(retorno);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet ("ReenviarFactura"), Authorize]
        public async Task<IActionResult> ReenviarFactura(string idFactura) 
        {
            try
            {
                var facturaEmail = await _ventas.consultarFactura(idFactura);
                facturaEmail.DetalleFacturas = await _ventas.ConsultarDetallePorFactura(facturaEmail.FtrId);
                var parametrosGlobales = await _parametros.ObtenerParametrosId(facturaEmail.FtrBodega);
                if (parametrosGlobales != null)
                {
                    _imprimir.Imprimir(facturaEmail,parametrosGlobales,true);
                }
                return Ok("Se hizo el envio del correo");
            }
            catch (Exception)
            {

                return BadRequest("Hubo un error a la hora de enviar");
            }
  

        }
    }
}
