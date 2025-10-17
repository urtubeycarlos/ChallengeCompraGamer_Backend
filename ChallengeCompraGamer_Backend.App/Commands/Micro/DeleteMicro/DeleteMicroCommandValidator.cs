using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.DeleteMicro
{
    public class DeleteMicroCommandValidator : AbstractValidator<DeleteMicroCommand>
    {
        public DeleteMicroCommandValidator()
        {
            RuleFor(x => x.Patente)
                .NotEmpty().WithMessage("La patente no puede estar vacía.")
                .MaximumLength(16).WithMessage("La patente no puede exceder los 16 caracteres.");
        }
    }
}
