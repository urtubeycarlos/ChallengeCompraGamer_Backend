using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.CreateMicro
{
    public class CreateMicroCommandValidator : AbstractValidator<CreateMicroCommand>
    {
        public CreateMicroCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotNull().WithMessage("El cuerpo de la solicitud no puede ser nulo.")
                .ChildRules(body =>
                {
                    body.RuleFor(b => b.Patente)
                        .NotEmpty().WithMessage("La patente no puede estar vacía.")
                        .MaximumLength(16).WithMessage("La patente no puede tener más de 16 caracteres.");
                    
                    body.RuleFor(b => b.Marca)
                        .NotEmpty().WithMessage("La marca no puede estar vacía.")
                        .MaximumLength(64).WithMessage("La marca no puede tener más de 64 caracteres.");

                    body.RuleFor(b => b.Modelo)
                        .NotEmpty().WithMessage("El modelo no puede estar vacío.")
                        .MaximumLength(64).WithMessage("El modelo no puede tener más de 64 caracteres.");

                    body.RuleFor(b => b.CantidadAsientos)
                        .GreaterThan(0).WithMessage("La cantidad de asientos debe ser mayor que cero.")
                        .LessThanOrEqualTo(70).WithMessage("La cantidad de asientos no puede ser mayor que 70.");
                });
        }
    }
}
