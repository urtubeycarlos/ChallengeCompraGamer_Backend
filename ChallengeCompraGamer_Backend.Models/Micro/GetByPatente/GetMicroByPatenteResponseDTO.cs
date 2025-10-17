using ChallengeCompraGamer_Backend.Models.Chico.GetAll;
using ChallengeCompraGamer_Backend.Models.Chofer.GetAll;

namespace ChallengeCompraGamer_Backend.Models.Micro.GetByPatente
{
    public class GetMicroByPatenteResponseDTO
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAsientos { get; set; }
        public int CantidadAsientosOcupados { get; set; }
        public bool EstaCompleto { get; set; }
        public bool TieneChofer { get; set; }
        public bool TieneChicos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<GetAllChicosResponseDTO> Chicos { get; set; } = new List<GetAllChicosResponseDTO>();
        public GetAllChoferesResponseDTO Chofer { get; set; } = null!;
    }
}
