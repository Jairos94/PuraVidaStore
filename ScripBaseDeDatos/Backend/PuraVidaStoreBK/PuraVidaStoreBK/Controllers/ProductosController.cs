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
        [HttpGet("ObtenerProductoPorId")]
        public async Task<IActionResult> ObtenerProductoPorId(int id) 
        {
            try
            {
                ProductosModel producto = (ProductosModel)ejecuta.ObtenerProductoPorId(id);
                return Ok(producto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
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
