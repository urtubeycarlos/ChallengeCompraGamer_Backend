using AutoMapper;
using ChallengeCompraGamer_Backend.DataAccess.Entities;
using ChallengeCompraGamer_Backend.Models.Chofer.Create;
using ChallengeCompraGamer_Backend.Models.Chofer.Delete;
using ChallengeCompraGamer_Backend.Models.Chofer.GetAll;
using ChallengeCompraGamer_Backend.Models.Chofer.GetByDNI;
using ChallengeCompraGamer_Backend.Models.Chofer.Update;

namespace ChallengeCompraGamer_Backend.Services.Maps
{
    public class ChoferMaps : Profile
    {
        public ChoferMaps()
        {
            CreateMap<Chofer, GetAllChoferesResponseDTO>();
            CreateMap<Chofer, GetChoferByDniResponseDTO>();

            CreateMap<CreateChoferRequestDTO, Chofer>();
            CreateMap<Chofer, CreateChoferResponseDTO>();

            CreateMap<UpdateChoferRequestDTO, Chofer>();
            CreateMap<Chofer, UpdateChoferResponseDTO>();

            CreateMap<Chofer, DeleteChoferResponseDTO>();
        }
    }
}
