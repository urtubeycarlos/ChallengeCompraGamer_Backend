namespace ChallengeCompraGamer_Backend.DataAccess.Entities
{
    public class Micro
    {
        public string Patente { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación con Chofer
        public string? ChoferDNI { get; set; }
        public Chofer? Chofer { get; set; }
    }
}
