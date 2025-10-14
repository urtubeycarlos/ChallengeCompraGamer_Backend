namespace ChallengeCompraGamer_Backend.DataAccess.Entities
{
    public class Micro
    {
        public string Patente { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación con Chico
        public ICollection<Chico> Chicos { get; set; } = new List<Chico>();
    }
}
