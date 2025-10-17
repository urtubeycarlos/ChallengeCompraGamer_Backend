using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.DeleteChico
{
    public class DeleteChicoCommandValidator : AbstractValidator<DeleteChicoCommand>
    {
        public DeleteChicoCommandValidator()
        {
            RuleFor(x => x.DNI)
                .NotEmpty().WithMessage("El DNI no puede estar vacío.")
                .MaximumLength(16).WithMessage("El DNI no puede tener más de 16 caracteres.");
        }
    }
}
