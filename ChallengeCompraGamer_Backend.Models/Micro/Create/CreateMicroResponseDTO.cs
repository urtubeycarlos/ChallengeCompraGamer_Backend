namespace ChallengeCompraGamer_Backend.Models.Micro.Create
{
    public class CreateMicroResponseDTO
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAsientos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
