using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using PuraVidaStoreBK.Models.Respuesta;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuariosQuerys _usuario;
        private readonly IPersonaQuery _persona;

        private IConfiguration Configuracion { get; }

        public UsuarioController(IConfiguration configuracion, 
            IMapper mapper,
            IUsuariosQuerys usuario,
            IPersonaQuery persona
            )
        {
            Configuracion = configuracion;
            _mapper = mapper;
            _usuario = usuario;
            _persona = persona;
        }



        #region peteciones
        // GET: UsuarioController
        [HttpGet("GetUsuario")]
        public async Task<IActionResult> GetUsuario(string user, string password)
        {

            var usuario = _usuario.GetUsuario(user, password);
            try
            {
                var UsuarioCaptura = new Usuario();
                UsuarioCaptura = (Usuario)usuario;
                usuarioRespuesta ur = new usuarioRespuesta();
                var Usuarioretorno = _mapper.Map<UsuarioDto>(UsuarioCaptura);
                Usuarioretorno.Persona = _mapper.Map<PersonaDto>(UsuarioCaptura.UsrIdPersonaNavigation);
                Usuarioretorno.Rol = _mapper.Map<RolUsuarioDto>(UsuarioCaptura.UsrIdRolNavigation);
                ur.usuario = Usuarioretorno;
                ur.token = CrearToken(Usuarioretorno);
                return Ok(ur);
            }
            catch (Exception)
            {
               
                return BadRequest(usuario);
            }

        }
        [HttpPost("GuardarUsario"), Authorize(Roles = "1")]
        public async Task<IActionResult> GuardarUsuario([FromBody] UsuarioDto UsuarioParametro, bool agregar)
        {
            var PersonaIngreso = _mapper.Map<Persona>(UsuarioParametro.Persona);
            var ListaPersonasPorCedula =await _persona.ObtenerPersonaPorCedula(PersonaIngreso.PsrIdentificacion);
            bool HuboError = false;
            string MensajeError="";
            var UsuarioIngreso = _mapper.Map<Usuario>(UsuarioParametro);
            

            //Valida si esta agregdo
            if (ListaPersonasPorCedula.Count == 0 || ListaPersonasPorCedula == null)
            {
                UsuarioIngreso.UsrIdPersonaNavigation = await _persona
                    .IngresarPersona(PersonaIngreso);

                UsuarioIngreso.UsrIdPersona = PersonaIngreso.PsrId;
            }
            UsuarioIngreso.UsrIdPersonaNavigation = PersonaIngreso;

            if (agregar) 
            {
                //Valida si es una persona por usuario 
                if (ValidarPersonaPorUsuario(ListaPersonasPorCedula)&& ListaPersonasPorCedula!=null) 
                {
                    HuboError = true;
                    MensajeError = "No se puede agregar el usuario porque esta persona ya tiene otro usuario";
                }

                //valida si el usuario ya se ingreso
                var UsuarioExiste = await ValidarUsuarioPorUsuario(UsuarioIngreso.UsrUser);
                if (UsuarioExiste) 
                {
                    HuboError = true;
                    MensajeError = "No se puede agregar al usuario porque el usuario escrito ya existe";
                }

                //Si no hubo errores en las vadicaciones se ingresa el usuario
                if (!HuboError) 
                {
                    
                    _usuario.IngresarUsario(UsuarioIngreso, UsuarioParametro.Clave);
                }
            }
            else 
            {
                var UsuarioBD =await _usuario.UsuarioPorId(UsuarioIngreso.UsrId);
                

                //valida si el usuario es diferente
                if (UsuarioIngreso.UsrUser != UsuarioBD.UsrUser) 
                {
                    //valida si el usuario cambiado y si otro usuario lo tiene 
                    var ExisteUsuario = await ValidarUsuarioPorUsuario(UsuarioIngreso.UsrUser);
                    if (ExisteUsuario) 
                    {
                        HuboError = true;
                        MensajeError = "No se puede editar al usuario porque el usuario escrito ya existe";
                    }

                }

                if (!HuboError) 
                {
                   await _persona.EditarPersona(PersonaIngreso);
                    _usuario.EditarUsuario(UsuarioIngreso, UsuarioParametro.UsrUser);
                   
                }

            }

            if (!HuboError) 
            {
                return Ok(true);
            }
            else 
            {
                return BadRequest(MensajeError);
            };
            
        }
        [HttpGet("ListaUsuarios"), Authorize(Roles = "1")]
        public async Task<IActionResult> ListaUsuarios()
        {
            var ListaUsuarios = await _usuario.ListaUsuarios();
            var UsuariosRetorno = _mapper.Map<List<UsuarioDto>>(ListaUsuarios);
            if (ListaUsuarios != null || ListaUsuarios.Count > 0) 
            {
                ListaUsuarios.ForEach(lu =>
                {
                    UsuariosRetorno.ForEach(ur =>
                    {
                        if (lu.UsrId == ur.UsrId)
                        {
                            ur.Clave = "";
                            ur.Persona = _mapper.Map<PersonaDto>(lu.UsrIdPersonaNavigation);
                            ur.Rol = _mapper.Map<RolUsuarioDto>(lu.UsrIdRolNavigation);
                        }
                    });
                });
                return Ok(UsuariosRetorno);
            }
            else 
            {
                return BadRequest("Se presento un error");
            }
            
        }

        // GET api/<UsuarioController>/5
        [HttpGet("UsuarioPorId"), Authorize]
        public async Task<IActionResult> UsuarioPorId(int id)
        {
            try
            {
                var UsuarioData =await _usuario.UsuarioPorId(id);
                var UsuarioRetorno = _mapper.Map<UsuarioDto>(UsuarioData);
                UsuarioRetorno.Clave = _usuario.ocpv(UsuarioRetorno.UsrId);
                UsuarioRetorno.Persona = _mapper.Map<PersonaDto>(UsuarioData.UsrIdPersonaNavigation);
                UsuarioRetorno.Rol = _mapper.Map<RolUsuarioDto>(UsuarioData.UsrIdRolNavigation);

                return Ok(UsuarioRetorno);           
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        #endregion

        #region Metodos privados
        //este metodo genera los tokens
        private string CrearToken(UsuarioDto Usuario) 
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,Usuario.UsrUser),
                new Claim(ClaimTypes.Role,Usuario.UsrIdRol.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                        Configuracion.GetSection("AppSettings:token").Value));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var Token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);

            return jwt;
        }

        private ActualizarTokenModel GetRefresh() 
        {
            var actualizar = new ActualizarTokenModel
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                FechaExpiro = DateTime.Now.AddDays(1),
                Creado=DateTime.Now

            };
            return actualizar;
        }

        private void enviarToken(ActualizarTokenModel nuevaActualizacionToken) 
        {
            var cookieOptions = new CookieOptions 
            {
                HttpOnly = true,
                Expires= nuevaActualizacionToken.FechaExpiro,

            };
            Response.Cookies.Append("actualizarToken",nuevaActualizacionToken.Token, cookieOptions);
        }

        private bool ValidarPersonaPorUsuario(List<Persona> PersonaExiste) 
        {
            bool Existe = false;
            //valida si la persona es usuario
            PersonaExiste.ForEach(x =>
            {
              
                var PersonaUsuario = _usuario.UsuarioIdPersona(x.PsrId);
                if (PersonaUsuario != null)
                {
                    Existe=   true;
                }
            });

            return Existe;
        }

        private async Task <bool> ValidarUsuarioPorUsuario(string UsuarioBusqueda) 
        {
            var Igual = false;
            var usuario = new Usuario();
            usuario = await _usuario.UsuarioPorUsuario(UsuarioBusqueda);
            if (usuario != null && usuario.UsrUser== UsuarioBusqueda) 
            {
                Igual = true;
            }
            return Igual;
        }

        #endregion

    }
}
