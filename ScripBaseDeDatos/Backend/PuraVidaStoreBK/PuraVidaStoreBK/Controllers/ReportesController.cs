﻿using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IReportesQuery _reportes;

        public ReportesController(IConfiguration configuration, IReportesQuery reportes)
        {
            _configuration = configuration;
            _reportes = reportes;
        }
        // GET: api/<ReportesController>
        [HttpGet ("ReporteMovimeintos"),Authorize]
        public async Task<IActionResult> ReporteMovimientos(int IdBodega,DateTime FechaInicio,DateTime FechaFin)
        {
            try
            {
                var listaReporte = new List<ReporteMovimientosDTO>();
                var listaMovimientos = await _reportes.ObtenerMovimientosPorBodega(IdBodega, FechaInicio, FechaFin);
                var listafacturas = await _reportes.ObtenerFacturasPorBodega(IdBodega, FechaInicio, FechaFin);

                foreach (var movimiento in listaMovimientos)
                {
                    var producto = new Producto();
                    producto = movimiento.MvmIdProductoNavigation;

                    var bodega = new Bodega();
                    bodega = movimiento.MvmIdBodegaNavigation;

                    var usuario = new Usuario();
                    usuario = movimiento.MvmIdUsuarioNavigation;
                    var reporte = new ReporteMovimientosDTO
                    {
                        Codigo = producto.PrdCodigo,
                        DescripcionProducto = producto.PrdNombre,
                        fecha = movimiento.MvmFecha,
                        Responsable = movimiento.MvmIdUsuarioNavigation.UsrUser,
                        Bodega = movimiento.MvmIdBodegaNavigation.BdgDescripcion,
                        Descripcion = movimiento.MvmIdMotivoMovimientoNavigation.MtmDescripcion,
                        Ingresos = movimiento.MvmIdMotivoMovimientoNavigation.MtmIdTipoMovimiento == 1 ? movimiento.MvmCantidad : 0,
                        Salidas = movimiento.MvmIdMotivoMovimientoNavigation.MtmIdTipoMovimiento == 2 ? movimiento.MvmCantidad : 0

                    };
                    listaReporte.Add(reporte);
                }

                foreach (var factura in listafacturas)
                {
                    foreach (var detalle in factura.DetalleFacturas)
                    {
                        var producto = new Producto();
                        producto = detalle.DtfIdProducto1;

                        var bodega = new Bodega();
                        bodega = factura.FtrBodegaNavigation;

                        var usuario= new Usuario();
                        usuario = factura.FtrIdUsuarioNavigation;

                        var reporte = new ReporteMovimientosDTO
                        {
                            Codigo = producto.PrdCodigo,
                            DescripcionProducto = detalle.DtfIdProducto1.PrdNombre,
                            fecha = factura.FtrFecha,
                            Responsable = usuario.UsrUser,
                            Bodega = bodega.BdgDescripcion,
                            Descripcion = factura.FtrEsFacturaNula != null && factura.FtrEsFacturaNula != true ? "Ventas N" + factura.FtrCodigoFactura: "Factura nula " + factura.FtrCodigoFactura,
                            Ingresos = 0,
                            Salidas = factura.FtrEsFacturaNula != null && factura.FtrEsFacturaNula != true ? detalle.DtfCantidad : 0

                        };
                        listaReporte.Add(reporte);
                    }
                };
                listaReporte.OrderBy(x=>x.fecha);

                return Ok(listaReporte);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
