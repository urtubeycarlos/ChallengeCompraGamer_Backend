namespace ChallengeCompraGamer_Backend.Models.Micro.Create
{
    public class CreateMicroRequestDTO
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAsientos { get; set; }
    }
}
