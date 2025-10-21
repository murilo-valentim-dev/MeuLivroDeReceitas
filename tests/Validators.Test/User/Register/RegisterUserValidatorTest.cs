using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Aplication.UseCases.User.Registrar;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            //SHOULDLY
            result.IsValid.ShouldBeTrue();
            ////FLUENT ASSERTIONS
            //result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            //SHOULDLY
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldSatisfyAllConditions(errors => errors.ShouldHaveSingleItem(),
                                                     errors => errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMassagesException.NAME_EMPTY)));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            //SHOULDLY
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldSatisfyAllConditions(errors => errors.ShouldHaveSingleItem(),
                                                     errors => errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMassagesException.EMAIL_EMPTY)));
        }


    }
}
