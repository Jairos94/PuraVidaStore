using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        ProductoQuery ejecuta = new ProductoQuery();
        [HttpGet("ListaProductos"), Authorize]
        public async Task<IActionResult> ListaProductos()
        {
            return Ok("");

        }

        [HttpPost("GuardarProducto"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarProducto([FromBody] ProductosModel model) 
        {
            try
            {
                model = (ProductosModel)ejecuta.Guardar(model);
                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
