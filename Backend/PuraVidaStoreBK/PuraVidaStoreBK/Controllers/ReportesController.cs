using Microsoft.AspNetCore.Mvc;
using PuraVidaStoreBK.ExecQuerys.Interfaces;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PuraVidaStoreBK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IReportesQuery _reportes;
        private readonly IVentasQuery _ventas;
        private readonly IBodegaQuery _bodega;

        public ReportesController(IConfiguration configuration, IReportesQuery reportes, IVentasQuery ventas, IBodegaQuery bodega)
        {
            _configuration = configuration;
            _reportes = reportes;
            _ventas = ventas;
            _bodega = bodega;
        }
        // GET: api/<ReportesController>
        [HttpGet("ReporteMovimeintos"), Authorize]
        public async Task<IActionResult> ReporteMovimientos(int IdBodega, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                var listaReporte = new List<ReporteMovimientosDTO>();
                var listaMovimientos = await _reportes.ObtenerMovimientosPorBodega(IdBodega, FechaInicio, FechaFin);
                var detallesFacturas = await _reportes.ObtenerDetallesFacturas(IdBodega, FechaInicio, FechaFin);

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

                foreach (var detalle in detallesFacturas)
                {
                    detalle.DtfIdProductoNavigation = await _ventas.consultarFactura(detalle.DtfIdFactura.ToString());
                    var bodega = new Bodega();
                    bodega =await _bodega.BodegaPorId(detalle.DtfIdProductoNavigation.FtrBodega);

                    var usuario = new Usuario();
                    usuario = detalle.DtfIdProductoNavigation.FtrIdUsuarioNavigation;
                    var producto = new Producto();
                    producto = detalle.DtfIdProducto1;

                    var cantidad = 0;

                    var descripcion = "";
                    if (detalle.DtfIdProductoNavigation.FtrEsFacturaNula != null && detalle.DtfIdProductoNavigation.FtrEsFacturaNula == true)
                    {
                        descripcion = "Factura nula N°" + detalle.DtfIdProductoNavigation.FtrCodigoFactura;
                        cantidad = 0;
                    }
                    else
                    {
                        if (detalle.DtfIdProductoNavigation.FtrEsFacturaNula != null && detalle.DtfIdProductoNavigation.FtrEsFacturaNula == false)
                        {
                            descripcion = "Ventas N°" + detalle.DtfIdProductoNavigation.FtrCodigoFactura;
                            cantidad = detalle.DtfCantidad;

                        }
                    }

                    var reporte = new ReporteMovimientosDTO
                    {
                        Codigo = producto.PrdCodigo,
                        DescripcionProducto = detalle.DtfIdProducto1.PrdNombre,
                        fecha = detalle.DtfIdProductoNavigation.FtrFecha,
                        Responsable = usuario.UsrUser,
                        Bodega = bodega.BdgDescripcion,
                        Descripcion = descripcion,
                        Ingresos = 0,
                        Salidas = cantidad

                    };
                    listaReporte.Add(reporte);
                   
                };
                listaReporte =listaReporte.OrderByDescending(x => x.fecha).ToList();

                return Ok(listaReporte);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("ReporteMovimientosPorProductos"),Authorize]
        public async Task<IActionResult> ReporteMovimientosPorProductos(int IdBodega, DateTime FechaInicio, DateTime FechaFin,string Codigo) 
        {
            try
            {
                var listaReporte = new List<ReporteMovimientosDTO>();
                var listaMovimientos = await _reportes.ObtenerMovimientosProdcto(IdBodega, FechaInicio, FechaFin, Codigo);
                var detallesFacturas = await _reportes.ObtenerDetallesFacturas(IdBodega, FechaInicio, FechaFin);
                detallesFacturas=detallesFacturas.Where(x=>x.DtfIdProducto1.PrdId.ToString()==Codigo || x.DtfIdProducto1.PrdCodigo==Codigo || x.DtfIdProducto1.PrdCodigoProvedor == Codigo).ToList();
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


                foreach (var detalle in detallesFacturas)
                {
                    detalle.DtfIdProductoNavigation = new Factura();
                    detalle.DtfIdProductoNavigation = await _ventas.consultarFactura(detalle.DtfIdFactura.ToString());
                    var bodega = new Bodega();
                    bodega = await _bodega.BodegaPorId(detalle.DtfIdProductoNavigation.FtrBodega);

                    var usuario = new Usuario();
                    usuario = detalle.DtfIdProductoNavigation.FtrIdUsuarioNavigation;
                    var producto = new Producto();
                    producto = detalle.DtfIdProducto1;

                    var cantidad = 0;

                    var descripcion = "";
                    if (detalle.DtfIdProductoNavigation.FtrEsFacturaNula != null && detalle.DtfIdProductoNavigation.FtrEsFacturaNula == true)
                    {
                        descripcion = "Factura nula N°" + detalle.DtfIdProductoNavigation.FtrCodigoFactura;
                        cantidad = 0;
                    }
                    else
                    {
                        if (detalle.DtfIdProductoNavigation.FtrEsFacturaNula != null && detalle.DtfIdProductoNavigation.FtrEsFacturaNula == false)
                        {
                            descripcion = "Ventas N°" + detalle.DtfIdProductoNavigation.FtrCodigoFactura;
                            cantidad = detalle.DtfCantidad;

                        }
                    }

                    var reporte = new ReporteMovimientosDTO
                    {
                        Codigo = producto.PrdCodigo,
                        DescripcionProducto = detalle.DtfIdProducto1.PrdNombre,
                        fecha = detalle.DtfIdProductoNavigation.FtrFecha,
                        Responsable = usuario.UsrUser,
                        Bodega = bodega.BdgDescripcion,
                        Descripcion = descripcion,
                        Ingresos = 0,
                        Salidas = cantidad

                    };
                    listaReporte.Add(reporte);

                };
                listaReporte = listaReporte.OrderByDescending(x => x.fecha).ToList();
       


                return Ok(listaReporte);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("ReporteVentasBodega"), Authorize]
        public async Task<IActionResult> ReporteMovimientosPorProductos(int IdBodega,DateTime FechaInicio,DateTime FechaFin) 
        {
            try
            {
                var reporte = new ReporteFacturaDTO();
                decimal montoNoNulas = 0;
                decimal montoNulas = 0;

                var facturas = await _reportes.ObtenerReporteVentas(IdBodega,FechaInicio,FechaFin);
                var listaFacturas = new List<ReporteFacturaListaDTO>();
                foreach (var factura in facturas)
                {
                    var facturaResumen = new FacturaResumen();
                    facturaResumen = factura.FacturaResumen.FirstOrDefault();
                    var descripcion = "";
                    if ((bool)factura.FtrEsFacturaNula) 
                    {
                        descripcion ="Factura se encuentra nula por la siguiente razón:\n" + factura.HistorialFacturasAnulada.FirstOrDefault().HlfRazon;
                        montoNulas += facturaResumen.FtrMontoTotal;
                    }
                    else 
                    {
                        descripcion = "Venta";
                        montoNoNulas += facturaResumen.FtrMontoTotal;
                    }
                    var facturaReporte = new ReporteFacturaListaDTO
                    {
                        CodigoFactura = factura.FtrCodigoFactura,
                        Usuario = factura.FtrIdUsuarioNavigation.UsrUser,
                        Fecha = factura.FtrFecha,
                        MontoFactura = facturaResumen.FtrMontoTotal,
                        DescripcionFactura = descripcion
                    };
                    listaFacturas.Add(facturaReporte);
                }
                reporte.MontoTotal = montoNoNulas;
                reporte.MontoTotalNulas = montoNoNulas;
                listaFacturas = listaFacturas.OrderByDescending(x=>x.Fecha).ToList();
                reporte.Lista = listaFacturas;
                return Ok(reporte);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
