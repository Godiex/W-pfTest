using FluentValidation;

namespace Application.UseCase.UpdateContactData
{
    public class UpdateContactDataValidator : AbstractValidator<UpdateContactDataCommand>
    {
        public UpdateContactDataValidator()
        {
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.");

            RuleFor(command => command.Phone)
                .NotEmpty().WithMessage("El número de teléfono es requerido.")
                .Matches(@"^\d{10}$").WithMessage("El número de teléfono debe contener exactamente 10 dígitos.");
        }
    }

}
