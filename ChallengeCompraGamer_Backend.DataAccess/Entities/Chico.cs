namespace ChallengeCompraGamer_Backend.DataAccess.Entities
{
    public class Chico
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación con Micro
        public string? PatenteMicro { get; set; }
        public Micro? Micro { get; set; }
    }
}
