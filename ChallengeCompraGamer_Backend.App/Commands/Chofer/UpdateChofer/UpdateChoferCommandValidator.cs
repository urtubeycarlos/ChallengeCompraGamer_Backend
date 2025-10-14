using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCompraGamer_Backend.App.Commands.Chofer.UpdateChofer
{
    public class UpdateChoferCommandValidator : AbstractValidator<UpdateChoferCommand>
    {
        private static string[] LicenciasHabilitadas = ["D", "D1", "D2", "D3", "E", "E1"];

        public UpdateChoferCommandValidator()
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

                    body.RuleFor(b => b.ClaseLicencia)
                        .NotEmpty().WithMessage("La clase de licencia no puede estar vacía.")
                        .MinimumLength(1).WithMessage("La clase de licencia debe tener al menos 1 carácter.")
                        .MaximumLength(2).WithMessage("La clase de licencia no puede tener más de 2 caracteres.")
                        .Must(licencia => LicenciasHabilitadas.Contains(licencia)).When(b => !string.IsNullOrEmpty(b.ClaseLicencia))
                        .WithMessage($"La clase de licencia debe ser una de las siguientes: {string.Join(", ", LicenciasHabilitadas)}.");

                    body.RuleFor(b => b.Telefono)
                        .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                        .Must(t => int.TryParse(t.ToString(), out int numero) && numero >= 0)
                        .WithMessage("El teléfono debe ser un número válido y no negativo.")
                        .Must(t => t.ToString().Length <= 16).WithMessage("El teléfono no puede tener más de 16 dígitos.");
                });
        }
    }
}
