using FluentValidation;

namespace Application.CreateUser
{

    using FluentValidation;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Identification)
                .NotEmpty().WithMessage("La identificación es requerida.")
                .Length(8, 10).WithMessage("La identificación debe tener entre 5 y 20 caracteres.");

            RuleFor(command => command.FullName)
                .NotEmpty().WithMessage("El nombre completo es requerido.")
                .MaximumLength(50).WithMessage("El nombre completo no puede superar los 50 caracteres.");

            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.");

            RuleFor(command => command.Phone)
                .NotEmpty().WithMessage("El número de teléfono es requerido.")
                .Matches(@"^\d{10}$").WithMessage("El número de teléfono debe contener exactamente 10 dígitos.");
        }
    }

}
