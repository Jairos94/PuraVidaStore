using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DTOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public RolesController(IMapper mapper, IRolesQuerys rolQuery)
        {
            _mapper = mapper;
            _rolQuery = rolQuery;
        }

        private readonly IMapper _mapper;
        private readonly IRolesQuerys _rolQuery;

        // GET: api/<RolesController>
        [HttpGet("ListaRoles"), Authorize]
        public async Task<IActionResult> ListaRoles()
        {
            var listaUsuarios =await _rolQuery.obtenerListaroles();
            var listaRetorno = _mapper.Map<List<RolUsuarioDto>>(listaUsuarios);
            return Ok(listaRetorno);
        }
    }
}
