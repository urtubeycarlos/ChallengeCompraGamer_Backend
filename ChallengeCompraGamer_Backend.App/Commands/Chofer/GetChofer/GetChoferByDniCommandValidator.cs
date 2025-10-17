using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.GetChofer
{
    public class GetChoferByDniCommandValidator : AbstractValidator<GetChoferByDniCommand>
    {
        public GetChoferByDniCommandValidator()
        {
            RuleFor(x => x.DNI)
                .NotEmpty().WithMessage("El DNI no puede estar vacío.")
                .MaximumLength(16).WithMessage("El DNI no puede tener más de 16 caracteres.");
        }
    }
}
