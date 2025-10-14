namespace ChallengeCompraGamer_Backend.Models.Chofer.Update
{
    public class UpdateChoferResponseDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ClaseLicencia { get; set; }
        public int Telefono { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
