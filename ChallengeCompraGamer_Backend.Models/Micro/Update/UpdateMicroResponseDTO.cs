namespace ChallengeCompraGamer_Backend.Models.Micro.Update
{
    public class UpdateMicroResponseDTO
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAsientos { get; set; }
        public bool EstaCompleto { get; set; }
        public bool EstaAsignado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
