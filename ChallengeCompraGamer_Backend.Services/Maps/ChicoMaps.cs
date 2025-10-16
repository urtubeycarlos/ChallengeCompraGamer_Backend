using AutoMapper;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Chico.Create;
using ChallengeCompraGamer_Backend.Models.Chico.Delete;
using ChallengeCompraGamer_Backend.Models.Chico.GetAll;
using ChallengeCompraGamer_Backend.Models.Chico.GetByDNI;
using ChallengeCompraGamer_Backend.Models.Chico.Update;

namespace ChallengeCompraGamer_Backend.Services.Maps
{
    public class ChicoMaps : Profile
    {
        public ChicoMaps()
        {
            CreateMap<Chico, GetAllChicosResponseDTO>()
                .ForMember(dest => dest.EstaAsignado, opt => opt.MapFrom(src => src.Micro != null));

            CreateMap<Chico, GetChicoByDniResponseDTO>()
                .ForMember(dest => dest.EstaAsignado, opt => opt.MapFrom(src => src.Micro != null));

            CreateMap<CreateChicoRequestDTO, Chico>();
            CreateMap<Chico, CreateChicoResponseDTO>();

            CreateMap<UpdateChicoRequestDTO, Chico>();
            CreateMap<Chico, UpdateChicoResponseDTO>();

            CreateMap<Chico, DeleteChicoResponseDTO>();
        }
    }
}
