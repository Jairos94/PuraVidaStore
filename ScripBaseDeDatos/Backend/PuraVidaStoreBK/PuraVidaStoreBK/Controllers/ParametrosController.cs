﻿using AutoMapper;
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
    public class ParametrosController : ControllerBase
    {
        private readonly IParametrosGeneralesQuery _parametros;
        private readonly IMapper _mapper;

        public ParametrosController(IParametrosGeneralesQuery parametros, IMapper mapper)
        {
            _parametros = parametros;
            _mapper = mapper;
        }

        [HttpPost("GuardarParametrosGlobales"), Authorize]
        public async Task<IActionResult> GuardarParametrosGlobales(ParametrosGlobalesDTO parametros)
        {
            try
            {
                var guardar = await _parametros.GuardarParametros(_mapper.Map<ParametrosGlobales>(parametros));

                var retorno = new ParametrosGlobalesDTO();
                retorno = _mapper.Map<ParametrosGlobalesDTO>(guardar);

                if (parametros.ImpuestosPorParametros.Count!=null) 
                {
                    foreach (var impuesto in parametros.ImpuestosPorParametros) 
                    {
                        impuesto.ImpPidParametroGlobal = retorno.PrgId;
                        await _parametros.GuardarImpuestoPorParametro(_mapper.Map<ImpuestosPorParametro>(impuesto));
                    }
                }

                var listaImpuesto =await _parametros.ObtenerImpuestosPorParametro(guardar.PrgId);

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
        [HttpGet("ObtenerParametros"),Authorize]
        public async Task<IActionResult> ObtenerParametros(int idBodega)
        {
            try
            {
                var respuesta = await _parametros.ObtenerParametrosId(idBodega);
                respuesta.ImpuestosPorParametros = await _parametros.ObtenerImpuestosPorParametro(respuesta.PrgId);
                return Ok(_mapper.Map<ParametrosGlobalesDTO>(respuesta));
            }
            catch (Exception ex)
            {

                return BadRequest("Favor de revisar los logs");
            }
            

        }
    }
}
