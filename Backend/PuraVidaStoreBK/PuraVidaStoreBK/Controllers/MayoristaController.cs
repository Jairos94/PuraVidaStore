using AutoMapper;
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
    public class MayoristaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMayoristaQuery _mayorista;
        private readonly IPersonaQuery _persona;

        public MayoristaController(IMapper mapper, IMayoristaQuery mayorista,IPersonaQuery persona)
        {
            _mapper = mapper;
            _mayorista = mayorista;
            _persona = persona;
        }
        // GET: api/<MayoristaController>
        [HttpGet("clienteMayoristaPorCedulaoId"),Authorize]
        public async Task<IActionResult> clienteMayoristaPorCedulaoId(string buscador)
        {
            try
            {
                var retorno = _mapper.Map<ClienteMayoristaDTO>(await _mayorista.buscarClientePorCedulaOId(buscador));
                if (retorno != null)
                {
                    return Ok(retorno);
                }
                else 
                {
                    return NoContent();
                }
               
            }
            catch (Exception)
            {

                return BadRequest("Favor de revisar los logs");
            }
        }

        // GET api/<MayoristaController>/5
        [HttpGet("ListaClienteMayorista"),Authorize]
        public async Task<IActionResult> ListaClienteMayorista()
        {
            var lista = _mapper.Map<List< ClienteMayoristaDTO>>(await _mayorista.listaClientesMayorista());
            if (lista.Count > 0)
            {
                return Ok(lista);
            }
            else 
            {
                return NoContent();
            }
            
        }

        // POST api/<MayoristaController>
        [HttpPost("GuardarClienteMayorista"),Authorize]
        public async Task<IActionResult> GuardarClienteMayorista([FromBody] ClienteMayoristaDTO clienteMayorista)
        {
            try
            {
                DateTime fechaActual = DateTime.Today;
                
                var  consultaCliente =await _mayorista.buscarClientePorCedulaOId(clienteMayorista.ClmIdPersonaNavigation.PsrIdentificacion);
                if (consultaCliente==null) 
                {
                    consultaCliente = new ClientesMayorista();
                }
                var consultaPersona = await _persona.BuscarUnaPersonaPorCedula(clienteMayorista.ClmIdPersonaNavigation.PsrIdentificacion);
                var persona = new Persona();

                if ((consultaCliente != null && consultaCliente.ClmIdPersona != 0) || (consultaPersona!=null && consultaPersona.PsrId!=0) )
                {
                    persona.PsrId = consultaPersona.PsrId;
                    persona.PsrNombre = clienteMayorista.ClmIdPersonaNavigation.PsrNombre;
                    persona.PsrApellido1 = clienteMayorista.ClmIdPersonaNavigation.PsrApellido1;
                    persona.PsrApellido2 = clienteMayorista.ClmIdPersonaNavigation.PsrApellido2;
                    persona.PsrIdentificacion = clienteMayorista.ClmIdPersonaNavigation.PsrIdentificacion;
                    persona = await _persona.EditarPersona(persona);
                }
                else 
                {
                    persona= await _persona.IngresarPersona(_mapper.Map<Persona>(clienteMayorista.ClmIdPersonaNavigation));
                }
                var mayorista = _mapper.Map<ClientesMayorista>(clienteMayorista);
                mayorista.ClmIdPersona = persona.PsrId;
                mayorista.ClmIdPersonaNavigation = null;
                mayorista.HistorialClienteMayorista = null;
                if (clienteMayorista.ClmFechaVencimiento== null) 
                {
                    mayorista.ClmFechaVencimiento = fechaActual;
                }
                
                mayorista = await _mayorista.guardarClienteMayorista(mayorista);

                if (clienteMayorista.ClmFechaVencimiento == null || mayorista.ClmFechaVencimiento < fechaActual)
                {
                    var nuevaFechaVencimiento = new DateTime();
                    switch (clienteMayorista.IdTipoTiempo)
                    {
                        case 1:
                            nuevaFechaVencimiento = fechaActual.AddDays(clienteMayorista.CantidadTiempo);
                            break;
                        case 2:
                            nuevaFechaVencimiento = fechaActual.AddMonths(clienteMayorista.CantidadTiempo);
                            break;
                        case 3:
                            nuevaFechaVencimiento = fechaActual.AddYears(clienteMayorista.CantidadTiempo);
                            break;
                    }
                    mayorista.ClmFechaVencimiento = nuevaFechaVencimiento;
                    HistorialClienteMayorista historial = new HistorialClienteMayorista();

                    historial.HcmId = 0;
                    historial.HcmIdCliente = mayorista.ClmId;
                    if (consultaCliente.ClmId != 0)
                    {
                        historial.HcmFechaVencimiento = consultaCliente.ClmFechaVencimiento;
                    }
                    else
                    {
                        historial.HcmFechaVencimiento = fechaActual;

                    }

                    historial.HcmFechaActualizacion = nuevaFechaVencimiento;
                    
                   await _mayorista.AgregarAlhistorial(historial);
                    mayorista = await _mayorista.guardarClienteMayorista(mayorista);
                }
                var retorno = _mapper.Map<ClienteMayoristaDTO>(await _mayorista.buscarClientePorCedulaOId(mayorista.ClmId.ToString()));
                return Ok(retorno);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("BuscarClientePorNombre"),Authorize]
        public async Task<IActionResult> BuscarClientePorNombre(string buscador) 
        {
            var lista = _mapper.Map< List< ClienteMayoristaDTO>>(await _mayorista.buscarClientePorNombre(buscador));
            return Ok(lista);
        }
    }
}
