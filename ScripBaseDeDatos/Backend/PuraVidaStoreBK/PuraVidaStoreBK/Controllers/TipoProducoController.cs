using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
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
                TipoProducto dato =(TipoProducto) ejecuta.Guardar(TipoProducto);
                return Ok(dato);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            

        }
    }
}
