using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProducoController : ControllerBase
    {
        [HttpPost("IngresarTipoProducto"), Authorize(Roles = "1")]
        public async Task<IActionResult> IngresarTipoProducto()
        {
            return Ok("");

        }
    }
}
