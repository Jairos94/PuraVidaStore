using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using PuraVidaStoreBK.Utilitarios.Interfase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly IParametrosGeneralesQuery _parametros;
        private readonly IMapper _mapper;
        private readonly IObtenerImpresoras _obtenerImpresoras;

        public ParametrosController(IParametrosGeneralesQuery parametros, IMapper mapper, IObtenerImpresoras obtenerImpresoras)
        {
            _parametros = parametros;
            _mapper = mapper;
            _obtenerImpresoras = obtenerImpresoras;
        }

        [HttpPost("GuardarParametrosGlobales"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarParametrosGlobales(ParametrosGlobalesDTO parametros)
        {
            try
            {
                var guardar = await _parametros.GuardarParametros(_mapper.Map<ParametrosGlobales>(parametros));

                var retorno = new ParametrosGlobalesDTO();
                retorno = _mapper.Map<ParametrosGlobalesDTO>(guardar);
                var listaImpuestoPorParametro = await _parametros.ObtenerImpuestosPorParametro(guardar.PrgId);
                if (listaImpuestoPorParametro.Count > 0)
                {
                    var seBorroLista = await _parametros.EliminarImpustoPorParametro(listaImpuestoPorParametro);
                }
                if (parametros.ImpuestosPorParametros != null)
                {
                    foreach (var impuesto in parametros.ImpuestosPorParametros)
                    {
                        impuesto.ImpPidParametroGlobal = retorno.PrgId;
                    }
                    await _parametros.GuardarImpuestoPorParametro(_mapper.Map<List<ImpuestosPorParametro>>(parametros.ImpuestosPorParametros));
                }
                if (parametros.ParametrosEmail != null)
                {
                    parametros.ParametrosEmail.PreIdParametroGlobal = guardar.PrgId;
                    var guardarCorreo = await _parametros.GuardarEmail(_mapper.Map<ParametrosEmail>(parametros.ParametrosEmail));
                }
                var listaImpuesto = await _parametros.ObtenerImpuestosPorParametro(guardar.PrgId);

                foreach (var impuesto in listaImpuesto)
                {
                    retorno.ImpuestosPorParametros.Add(_mapper.Map<ImpuestosPorParametroDTO>(impuesto));
                }
                return Ok(retorno);
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // GET api/<ParametrosController>/5
        [HttpGet("ObtenerParametros"), Authorize]
        public async Task<IActionResult> ObtenerParametros(int idBodega)
        {
            try
            {
                var respuesta = await _parametros.ObtenerParametrosId(idBodega);
                if (respuesta != null)
                {
                    respuesta.ImpuestosPorParametros = await _parametros.ObtenerImpuestosPorParametro(respuesta.PrgId);
                }

                return Ok(_mapper.Map<ParametrosGlobalesDTO>(respuesta));
            }
            catch (Exception ex)
            {

                return BadRequest("Favor de revisar los logs");
            }


        }

        [HttpGet("ListaTiempoParaRenovar"), Authorize]
        public async Task<IActionResult> ListaTiempoParaRenovar()
        {

            return Ok(_mapper.Map<List<TiempoParaRenovarDTO>>(await _parametros.ListaTiempoParaRenovar()));
        }

        [HttpGet("ObtnerImpresar"), Authorize]
        public async Task<IActionResult> ObtnerImpresar()
        {
            try
            {
                var lista = _obtenerImpresoras.Impresoras();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return BadRequest("Favor de revisar los logs");
            }

        }
    }
}
