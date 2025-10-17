namespace ChallengeCompraGamer_Backend.Models.Chofer.Create
{
    public class CreateChoferRequestDTO
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ClaseLicencia { get; set; }
        public int Telefono { get; set; }
    }
}
