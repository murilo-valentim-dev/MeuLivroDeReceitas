﻿using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecipeBook.Aplication.UseCases.User.Registrar;
using MyRecipeBook.Domain.Repositories;
using System.Threading.Tasks;

namespace UseCase.Test.User.Register
{
    public class RegisterUserCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        private RegisterUserUseCase CreateUseCase()
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UniteOfWorkBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder().Build();

            return new RegisterUserUseCase(writeRepository, readRepository, unitOfWork, passwordEncripter, mapper);
        }

    }
}
