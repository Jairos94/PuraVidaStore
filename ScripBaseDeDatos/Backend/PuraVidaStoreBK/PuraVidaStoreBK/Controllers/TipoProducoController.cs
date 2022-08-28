﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProducoController : ControllerBase
    {
        private TipoProductoQuery ejecuta = new TipoProductoQuery();

        [HttpPost("GuardarTipoProducto"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarTipoProducto([FromBody] TipoProducto TipoProducto)
        {
            try
            {
                TipoProducto Tipo = (TipoProducto)ejecuta.Guardar(TipoProducto);
                
                return Ok(Tipo);
            }
            catch (Exception ex)
            {

                return BadRequest( ex);
            }


        }

        [HttpGet("ListaTipoProducto"),Authorize]
       public async Task<IActionResult> ListaTipoProducto() 
        {
            List<TipoProducto> listaTipoProducto = new List<TipoProducto>();
            try
            {
                var listaModelo = new List<TipoProductoModel>();
                listaTipoProducto=(List<TipoProducto>) ejecuta.ListaTipoProducto();
                listaTipoProducto.ForEach(tp => 
                {
                    var tipoModelo = new TipoProductoModel();
                    tipoModelo.TppId = tp.TppId;
                    tipoModelo.TppDescripcion = tp.TppDescripcion;
                    tipoModelo.TppVisible = tp.TppVisible;
                    listaModelo.Add(tipoModelo);
                });
                return Ok(listaModelo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpGet("ObtenerTipoProductoPorId")]
        public async Task<IActionResult> ObtenerTipoProductoPorId(int id) 
        {
            try
            {
                TipoProducto data = (TipoProducto)ejecuta.TipoProductoPorId(id);
                var retorno = new TipoProductoModel 
                {
                    TppId=data.TppId,
                    TppDescripcion=data.TppDescripcion,
                    TppVisible= data.TppVisible
                };
                return Ok(retorno);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
          
        }
    }
}
