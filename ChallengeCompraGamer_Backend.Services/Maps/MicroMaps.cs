using AutoMapper;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChicos;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChofer;
using ChallengeCompraGamer_Backend.Models.Micro.Create;
using ChallengeCompraGamer_Backend.Models.Micro.Delete;
using ChallengeCompraGamer_Backend.Models.Micro.GetAll;
using ChallengeCompraGamer_Backend.Models.Micro.GetByPatente;
using ChallengeCompraGamer_Backend.Models.Micro.Update;

namespace ChallengeCompraGamer_Backend.Services.Maps
{
    public class MicroMaps : Profile
    {
        public MicroMaps()
        {
            CreateMap<CreateMicroRequestDTO, Micro>();
            
            CreateMap<Micro, GetAllMicrosResponseDTO>()
                .ForMember(dest => dest.CantidadAsientosOcupados, opt => opt.MapFrom(src => src.Chicos.Count()))
                .ForMember(dest => dest.EstaCompleto, opt => opt.MapFrom(src => src.Chicos.Count() >= src.CantidadAsientos))
                .ForMember(dest => dest.EstaAsignado, opt => opt.MapFrom(src => src.Chofer != null));

            CreateMap<Micro, GetMicroByPatenteResponseDTO>()
                .ForMember(dest => dest.CantidadAsientosOcupados, opt => opt.MapFrom(src => src.Chicos.Count()))
                .ForMember(dest => dest.EstaCompleto, opt => opt.MapFrom(src => src.Chicos.Count() >= src.CantidadAsientos))
                .ForMember(dest => dest.EstaAsignado, opt => opt.MapFrom(src => src.Chofer != null));

            CreateMap<Micro, CreateMicroResponseDTO>();

            CreateMap<UpdateMicroRequestDTO, Micro>();
            CreateMap<Micro, UpdateMicroResponseDTO>();
            
            CreateMap<Micro, DeleteMicroResponseDTO>();

            CreateMap<Micro, AssignChicosToMicroResponseDTO>()
                .ForMember(dest => dest.CantidadAsientosOcupados, opt => opt.MapFrom(src => src.Chicos.Count()))
                .ForMember(dest => dest.EstaCompleto, opt => opt.MapFrom(src => src.Chicos.Count() >= src.CantidadAsientos))
                .ForMember(dest => dest.EstaAsignado, opt => opt.MapFrom(src => src.Chofer != null));

            CreateMap<Micro, AssignChoferToMicroResponseDTO>()
                .ForMember(dest => dest.CantidadAsientosOcupados, opt => opt.MapFrom(src => src.Chicos.Count()))
                .ForMember(dest => dest.EstaCompleto, opt => opt.MapFrom(src => src.Chicos.Count() >= src.CantidadAsientos))
                .ForMember(dest => dest.EstaAsignado, opt => opt.MapFrom(src => src.Chofer != null));
        }
    }
}
