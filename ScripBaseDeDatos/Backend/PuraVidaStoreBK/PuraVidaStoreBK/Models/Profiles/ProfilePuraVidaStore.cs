using AutoMapper;
using PuraVidaStoreBK.Models.DbContex;
using PuraVidaStoreBK.Models.DTOS;

namespace PuraVidaStoreBK.Models
{
    public class ProfilePuraVidaStore:Profile
    {
        public ProfilePuraVidaStore()
        {
            
            CreateMap<RolUsiario, RolUsuarioDto>();
            CreateMap<RolUsuarioDto, RolUsiario>();

            CreateMap<Persona, PersonaDto>();
            CreateMap<PersonaDto, Persona>();

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<TipoProducto, TipoProductoDTO>();
            CreateMap<TipoProductoDTO, TipoProducto>();

        }
    }
}
