namespace ChallengeCompraGamer_Backend.DataAccess.Entities
{
    public class Micro
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAsientos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación con Chico
        public ICollection<Chico> Chicos { get; set; } = new List<Chico>();

        // Relación con Chofer
        public string? ChoferDNI { get; set; }
        public Chofer? Chofer { get; set; }
    }
}
