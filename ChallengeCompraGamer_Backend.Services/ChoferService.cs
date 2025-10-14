using ChallengeCompraGamer_Backend.Models.Micro.CreateMicro;

namespace ChallengeCompraGamer_Backend.Services
{
    public class ChoferService
    {
        private readonly CreateMicroRequestDTO _request;

        public ChoferService(CreateMicroRequestDTO request)
        {
            request = _request;
        }
    }
}
