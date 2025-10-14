using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.GetChico
{
    public class GetChicoByDniCommandValidator : AbstractValidator<GetChicoByDniCommand>
    {
        public GetChicoByDniCommandValidator()
        {
            RuleFor(x => x.DNI)
                .NotEmpty().WithMessage("El DNI no puede estar vacío.")
                .MaximumLength(16).WithMessage("El DNI no puede tener más de 16 caracteres.");
        }
    }
}
