using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.AssignChicosToMicro
{
    public class AssignChicosToMicroCommandValidator : AbstractValidator<AssignChicosToMicroCommand>
    {
        public AssignChicosToMicroCommandValidator()
        {
            RuleFor(x => x.Patente)
                .NotEmpty().WithMessage("La patente no puede estar vacía.")
                .MaximumLength(16).WithMessage("La patente no puede tener más de 16 caracteres.");

            When(x => x.Body != null, () =>
            {
                When(x => x.Body.DNIS != null && x.Body.DNIS.Count > 0, () =>
                {
                    RuleForEach(x => x.Body.DNIS).ChildRules(dni =>
                    {
                        dni.RuleFor(d => d)
                            .NotEmpty().WithMessage("El DNI no puede estar vacío.")
                            .MaximumLength(16).WithMessage("El DNI no puede tener más de 16 caracteres.");
                    });
                });                
            });
        }
    }
}
