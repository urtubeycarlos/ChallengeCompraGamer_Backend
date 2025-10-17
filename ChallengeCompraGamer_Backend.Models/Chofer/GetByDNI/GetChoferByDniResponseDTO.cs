using ChallengeCompraGamer_Backend.Models.Micro.GetAll;

namespace ChallengeCompraGamer_Backend.Models.Chofer.GetByDNI
{
    public class GetChoferByDniResponseDTO
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ClaseLicencia { get; set; }
        public int Telefono { get; set; }
        public bool EstaAsignado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public GetAllMicrosResponseDTO Micro { get; set; }
    }
}
