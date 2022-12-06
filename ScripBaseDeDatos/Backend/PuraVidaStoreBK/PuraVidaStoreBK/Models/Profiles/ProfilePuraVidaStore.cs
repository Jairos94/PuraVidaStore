using AutoMapper;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;

namespace PuraVidaStoreBK.Models
{
    public class ProfilePuraVidaStore:Profile
    {
        public ProfilePuraVidaStore()
        {
            CreateMap<Bodega, BodegaDTO>();
            CreateMap<BodegaDTO, Bodega>();

            CreateMap<Movimiento, MovimientosDTO>();
            CreateMap<MovimientosDTO, Movimiento>();

            CreateMap<MotivosMovimiento, MotivosMovimientoDTO>();
            CreateMap<MotivosMovimientoDTO, MotivosMovimiento>();

            CreateMap<Persona, PersonaDto>();
            CreateMap<PersonaDto, Persona>();

            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>();

            CreateMap<RolUsiario, RolUsuarioDto>();
            CreateMap<RolUsuarioDto, RolUsiario>();

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<TipoMovimiento, TipoMovimientoDTO>();
            CreateMap<TipoMovimientoDTO, TipoMovimiento>();

            CreateMap<TipoProducto, TipoProductoDTO>();
            CreateMap<TipoProductoDTO, TipoProducto>();

        }
    }
}
