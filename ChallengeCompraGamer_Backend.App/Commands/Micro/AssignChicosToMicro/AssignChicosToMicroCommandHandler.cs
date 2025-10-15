using MediatR;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChicos;
using ChallengeCompraGamer_Backend.Models;
using ChallengeCompraGamer_Backend.Services;
using ChallengeCompraGamer_Backend.Models.Micro.AssignChofer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChicosToMicro
{
    public class AssignChicosToMicroCommandHandler : IRequestHandler<AssignChicosToMicroCommand, Result<AssignChicosToMicroResponseDTO>>
    {
        private readonly MicroService _service;
        private readonly ILogger<AssignChicosToMicroCommandHandler> _logger;

        public AssignChicosToMicroCommandHandler(MicroService service, ILogger<AssignChicosToMicroCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result<AssignChicosToMicroResponseDTO>> Handle(AssignChicosToMicroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AssignChicosToMicroResponseDTO response = await _service.AssignChicosToMicro(request.Patente, request.Body);
                return Result<AssignChicosToMicroResponseDTO>.Success(response);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message, request.Patente);
                return Result<AssignChicosToMicroResponseDTO>.Failure(ex.Message, HttpStatusCode.NotFound);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return Result<AssignChicosToMicroResponseDTO>.Failure(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error asignando chicos al micro: {Patente}", request.Patente);
                return Result<AssignChicosToMicroResponseDTO>.Failure($"Error asignando chicos al micro: {request.Patente}. Por favor contacte al administrador.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
