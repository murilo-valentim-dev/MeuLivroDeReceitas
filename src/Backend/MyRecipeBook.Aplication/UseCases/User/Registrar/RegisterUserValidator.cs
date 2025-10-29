using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Aplication.UseCases.User.Registrar
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator() 
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMassagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMassagesException.EMAIL_EMPTY);
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMassagesException.PASSWORD_EMPTY);
            When(user => user.Email.NotEmpty(), () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMassagesException.EMAIL_INVALID);
            });
        }
    }
}
