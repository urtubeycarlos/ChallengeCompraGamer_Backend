using AutoMapper;
using ChallengeCompraGamer_Backend.Models.Chico.Create;
using ChallengeCompraGamer_Backend.Models.Chico.Delete;
using ChallengeCompraGamer_Backend.Models.Chico.GetAll;
using ChallengeCompraGamer_Backend.Models.Chico.GetByDNI;
using ChallengeCompraGamer_Backend.Models.Chico.Update;
using ChicoEntity = ChallengeCompraGamer_Backend.DataAccess.Entities.Chico;

namespace ChallengeCompraGamer_Backend.Models.Maps
{
    public class ChicoMaps : Profile
    {
        public ChicoMaps()
        {
            CreateMap<ChicoEntity, GetAllChicosResponseDTO>();
            CreateMap<ChicoEntity, GetChicoByDniResponseDTO>();

            CreateMap<CreateChicoRequestDTO, ChicoEntity>();
            CreateMap<ChicoEntity, CreateChicoResponseDTO>();

            CreateMap<UpdateChicoRequestDTO, ChicoEntity>();
            CreateMap<ChicoEntity, UpdateChicoResponseDTO>();

            CreateMap<ChicoEntity, DeleteChicoResponseDTO>();
        }
    }
}
