using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        RolesQuerys Ejecuta = new RolesQuerys();
        // GET: api/<RolesController>
        [HttpGet("ListaRoles"), Authorize]
        public List<RolModel> ListaRoles()
        {
            return Ejecuta.listaRoles();
        }
    }
}
