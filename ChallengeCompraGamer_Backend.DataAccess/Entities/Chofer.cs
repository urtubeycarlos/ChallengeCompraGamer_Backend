namespace ChallengeCompraGamer_Backend.DataAccess.Entities
{
    public class Chofer
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ClaseLicencia { get; set; }
        public int Telefono { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relación con Micro
        public Micro? Micro { get; set; }
    }
}
