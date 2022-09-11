using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PuraVidaStoreBK.ExecQuerys;
using PuraVidaStoreBK.Models;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.Respuesta;
using PuraVidaStoreBK.Utilitarios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuariosQuerys Ejecuta = new UsuariosQuerys();
        personaQuery EjecutaPersona = new personaQuery();

        public IConfiguration Configuracion { get; }

        public UsuarioController(IConfiguration configuracion) 
        {
            Configuracion = configuracion;
        }

        #region peteciones
        // GET: UsuarioController
        [HttpGet("GetUsuario")]
        public async Task<IActionResult> GetUsuario(string user, string password)
        {
            object Usu = new object();
            Usu = Ejecuta.GetUsuario(user, password);
            try
            {
                usuarioRespuesta ur = new usuarioRespuesta();
                ur.usuario = (UsuarioModel)Usu;
                ur.token = CrearToken((UsuarioModel)Usu);
                return Ok(ur);
            }
            catch (Exception)
            {

                return BadRequest(Usu);
            }

        }
        [HttpPost("GuardarUsario"), Authorize(Roles = "1")]
        public ActionResult GuardarUsuario([FromBody] UsuarioModel usuarioM, bool agregar)
        {
            Usuario u = new Usuario();
            Persona p = new Persona();
            PersonaModel pm1 = new PersonaModel();

            pm1.PsrIdentificacion = p.PsrIdentificacion = usuarioM.persona.PsrIdentificacion;
            pm1.PsrNombre = p.PsrNombre = usuarioM.persona.PsrNombre;
            pm1.PsrApellido1 = p.PsrApellido1 = usuarioM.persona.PsrApellido1;
            pm1.PsrApellido2 = p.PsrApellido2 = usuarioM.persona.PsrApellido2;

            //Guarda o actualiza a la persona desde el usuario
            if (usuarioM.IdPersona == 0)
            {
                List<PersonaModel> p2 = new List<PersonaModel>();
                p2 = (List<PersonaModel>)EjecutaPersona.obtenerPersonaPorCedula(p.PsrIdentificacion);

                //valida la cèdula que no sean usuarios
                if (p2.Count > 0)
                {
                    if (p2[0].PsrIdentificacion == p.PsrIdentificacion && p2 != null)
                    {
                        Usuario u2 = new Usuario();
                        u2 = (Usuario)Ejecuta.UsuarioIdPersona(p2[0].PsrId);
                        if (p2[0].PsrId == u2.UsrIdPersona)
                        {
                            return BadRequest("No se puede guardar el usuario porque otro usuario tiene la misma cédula");
                        }

                    }

                }
                pm1 = (PersonaModel)EjecutaPersona.ingresarPersona(pm1);
                p.PsrId = pm1.PsrId;
            }
            else
            {
                p.PsrId = usuarioM.IdPersona;
                p = (Persona)EjecutaPersona.EditarPersona(p);
            }
            //Valida si es agregar usuario
            if (agregar)
            {
                try
                {
                    u = (Usuario)Ejecuta.UsuarioPorUsuario(usuarioM.Usuario);
                }
                catch (Exception)
                {

                    u = null;
                }
                if (u != null)
                {
                    if (u.UsrUser == usuarioM.Usuario)
                    {
                        return BadRequest("No se puede guardar a este usuario debido que ya existe");
                    }
                }
                usuarioM.IdPersona = p.PsrId;
                Ejecuta.IngresarUsario(usuarioM);
            }
            //Valida se es editar
            else
            {
                UsuarioModel um = new UsuarioModel();
                PersonaModel pm = new PersonaModel();

                um = (UsuarioModel)Ejecuta.UsuarioPorId((int)usuarioM.IdUsuario);

                //valida si el usuario cambio existe
                if (usuarioM.IdUsuario == um.IdUsuario && usuarioM.Usuario != um.Usuario)
                {
                    u = (Usuario)Ejecuta.UsuarioPorUsuario(usuarioM.Usuario);
                    if (u.UsrUser == usuarioM.Usuario)
                    {
                        return BadRequest("No se puede guardar a este usuario debido que ya existe");
                    }
                }
                Ejecuta.EditarUsuario(usuarioM);

            }



            return Ok(usuarioM);
        }
        [HttpGet("ListaUsuarios"), Authorize(Roles = "1")]
        public async Task<IActionResult> ListaUsuarios()
        {
            object Usu = new object();
            Usu = Ejecuta.listaUsuarios();
            try
            {
                return Ok(Usu);
            }
            catch (Exception)
            {

                return BadRequest(Usu);
            }

        }

        // GET api/<UsuarioController>/5
        [HttpGet("UsuarioPorId"), Authorize]
        public ActionResult UsuarioPorId(int id)
        {
            try
            {
                return Ok(Ejecuta.UsuarioPorId(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // GET api/<UsuarioController>/5
        [HttpGet("UsuarioPorId2"), Authorize(Roles = "1")]
        public ActionResult UsuarioPorId2(int id)
        {
            try
            {
                return Ok(Ejecuta.UsuarioPorId2(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        #endregion

        #region Metodos privados
        //este metodo genera los tokens
        private string CrearToken(UsuarioModel Usuario) 
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,Usuario.Usuario),
                new Claim(ClaimTypes.Role,Usuario.IdRol.ToString())
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

        #endregion

    }
}
