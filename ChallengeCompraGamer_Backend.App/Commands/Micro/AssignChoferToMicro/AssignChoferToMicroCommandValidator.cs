using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChoferToMicro
{
    public class AssignChoferToMicroCommandValidator : AbstractValidator<AssignChoferToMicroCommand>
    {
        public AssignChoferToMicroCommandValidator()
        {
            RuleFor(x => x.Patente)
                .NotEmpty().WithMessage("La patente no puede estar vacía.")
                .MaximumLength(16).WithMessage("La patente no puede tener más de 16 caracteres.");

            When(x => x.Body != null, () =>
            {
                When(x => !string.IsNullOrEmpty(x.Body.DNI), () =>
                {
                    RuleFor(x => x.Body.DNI)
                        .MinimumLength(1).WithMessage("El DNI del chofer debe tener al menos 1 caracter.")
                        .MaximumLength(16).WithMessage("El DNI del chofer no puede tener más de 16 caracteres.");
                });
            });
        }
    }
}
