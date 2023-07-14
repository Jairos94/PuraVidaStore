using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ReportesController>
        [HttpGet ("ReporteMovimeintos")]
        public async Task<IActionResult> ReporteMovimientos(int IdBodega,DateTime FechaInicio,DateTime FechaFin)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var url = string.Format("{0}{1}", _configuration["Reportes:UrlPrincipal"], _configuration["Reportes:Movimientos"]);

                UriBuilder uriBuilder = new UriBuilder(url);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                query["IdBodega"] = IdBodega.ToString();
                query["FechaInicio"] = FechaInicio.ToString();
                query["FechaFin"] = FechaFin.ToString();
                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();

                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    byte[] reporteBytes = await response.Content.ReadAsByteArrayAsync();

                    return File(reporteBytes, "application/pdf", "reporte.pdf");
                }
                else
                {
                    // Manejo de errores si la solicitud no fue exitosa
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
    }
}
