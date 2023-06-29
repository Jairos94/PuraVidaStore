using AutoMapper;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;

namespace PuraVidaStoreBK.Models
{
    public class ProfilePuraVidaStore:Profile
    {
        public ProfilePuraVidaStore()
        {
            CreateMap<Bodega, BodegaDTO>().ReverseMap();

            CreateMap<ClientesMayorista, ClienteMayoristaDTO>().ReverseMap();

            CreateMap<DetalleFactura, DetalleFacturaDTO>().ReverseMap();

            CreateMap<EstatusFactura, EstatusFacturaDTO>().ReverseMap();

            CreateMap<Factura, FacturaDTO>().ReverseMap();

            CreateMap<FacturaResumen, FacturaResumenDTO>().ReverseMap();

            CreateMap<FormaPago, FormaPagoDTO>().ReverseMap();

            CreateMap<Impuesto, ImpuestosDTO>().ReverseMap();

            CreateMap<ImpuestosPorParametro, ImpuestosPorParametroDTO>().ReverseMap();

            CreateMap<ImpuestosPorFactura, ImpuestosPorFacturaDTO>().ReverseMap();

            CreateMap<Inventarios, InventariosDTO>().ReverseMap();

            CreateMap<HistorialClienteMayorista, HistorialClienteMayoristaDTO>().ReverseMap();

            CreateMap<Movimiento, MovimientosDTO>().ReverseMap();

            CreateMap<MotivosMovimiento, MotivosMovimientoDTO>().ReverseMap();

            CreateMap<ParametrosEmail, ParametrosEmailDTO>().ReverseMap();

            CreateMap<ParametrosGlobales, ParametrosGlobalesDTO>().ReverseMap();

            CreateMap<Persona, PersonaDto>().ReverseMap();

            CreateMap<Producto, ProductoDTO>().ReverseMap();

            CreateMap<RolUsiario, RolUsuarioDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            CreateMap<TiempoParaRenovar, TiempoParaRenovarDTO>().ReverseMap();

            CreateMap<TipoMovimiento, TipoMovimientoDTO>().ReverseMap();

            CreateMap<TipoProducto, TipoProductoDTO>().ReverseMap();

           

        }
    }
}
