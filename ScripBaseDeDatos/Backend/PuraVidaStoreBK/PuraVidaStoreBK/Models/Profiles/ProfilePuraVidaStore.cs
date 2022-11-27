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

            CreateMap<RolUsiario, RolUsuarioDto>();
            CreateMap<RolUsuarioDto, RolUsiario>();

            CreateMap<Persona, PersonaDto>();
            CreateMap<PersonaDto, Persona>();

            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>();

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<TipoProducto, TipoProductoDTO>();
            CreateMap<TipoProductoDTO, TipoProducto>();

        }
    }
}
