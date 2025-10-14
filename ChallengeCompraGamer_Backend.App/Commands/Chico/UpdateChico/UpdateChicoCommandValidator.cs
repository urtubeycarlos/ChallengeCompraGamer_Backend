using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Chico.UpdateChico
{
    public class UpdateChicoCommandValidator : AbstractValidator<UpdateChicoCommand>
    {
        public UpdateChicoCommandValidator()
        {
            RuleFor(x => x.DNI)
                .NotEmpty().WithMessage("El DNI no puede estar vacío.")
                .MaximumLength(16).WithMessage("El DNI no puede tener más de 16 caracteres.");

            RuleFor(x => x.Body)
                .NotNull().WithMessage("El cuerpo de la solicitud no puede ser nulo.")
                .ChildRules(body =>
                {
                    body.RuleFor(b => b.Nombre)
                        .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                        .MaximumLength(128).WithMessage("El nombre no puede tener más de 128 caracteres.");
                    body.RuleFor(b => b.Apellido)
                        .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                        .MaximumLength(128).WithMessage("El apellido no puede tener más de 128 caracteres.");
                });
        }
    }
}
