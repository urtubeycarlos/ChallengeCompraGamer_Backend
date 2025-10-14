using FluentValidation;

namespace ChallengeCompraGamer_Backend.App.Commands.Micro.GetMicro
{
    public class GetMicroCommandValidator : AbstractValidator<GetMicroCommandRequest>
    {
        public GetMicroCommandValidator()
        {
            RuleFor(x => x.Patente).NotEmpty().WithMessage("La patente no puede estar vacía.");
            RuleFor(x => x.Patente).MaximumLength(16).WithMessage("La patente no puede tener más de 16 caracteres.");
        }
    }
}
