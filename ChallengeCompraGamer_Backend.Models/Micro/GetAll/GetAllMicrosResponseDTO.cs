namespace ChallengeCompraGamer_Backend.Models.Micro.GetAll
{
    public class GetAllMicrosResponseDTO
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAsientos { get; set; }
        public int CantidadAsientosOcupados { get; set; }
        public bool EstaCompleto { get; set; }
        public bool EstaAsignado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
