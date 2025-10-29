using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;
using MyRecipeBook.Aplication.Services.AutoMapper;
using MyRecipeBook.Aplication.Services.Cryptography;
using MyRecipeBook.Domain.Repositories.User;
using AutoMapper;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Domain.Extensions;


namespace MyRecipeBook.Aplication.UseCases.User.Registrar
{
    public  class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;
        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, 
            IUserReadOnlyRepository readOnlyRepository,
            IUnitOfWork unitOfWork,
            PasswordEncripter passwordEncripter, 
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            //validar a request
            await Validate(request);
            //mapear a request em uma entidade
            var user = _mapper.Map<Domain.Entities.User>(request);
            // Criptografia da senha
            user.Password = _passwordEncripter.Encrypt(request.Password);
            // Salvar no banco de dados
            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWithWmail(request.Email);
            if (emailExist) 
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMassagesException.EMAIL_ALREADY_REGISTRED));
            

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}

   
