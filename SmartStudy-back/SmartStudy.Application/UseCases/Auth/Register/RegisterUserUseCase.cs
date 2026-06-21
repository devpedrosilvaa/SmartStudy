using SmartStudy.Application.Interfaces.Repositories;
using SmartStudy.Application.Interfaces.Security;
using SmartStudy.Domain.Common;
using SmartStudy.Domain.Common.Errors;

namespace SmartStudy.Application.UseCases.Auth.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> ExecuteAsync(RegisterUserRequest request)
        {
            var existingUser = await _userRepository.GetByEmail(request.Email);
            if(existingUser is not null)
                return Result.Failure(UserError.UserEmailAlreadyExists);

            var passwordHash = _passwordHasher.Execute(request.Password);

            var user = new Domain.Entities.User(
                request.Name,
                request.Email,
                passwordHash
            );

            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
