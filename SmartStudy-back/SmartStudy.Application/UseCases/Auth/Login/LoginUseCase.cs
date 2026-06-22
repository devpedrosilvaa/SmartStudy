using SmartStudy.Application.Interfaces.Repositories;
using SmartStudy.Application.Interfaces.Security;
using SmartStudy.Domain.Common;
using SmartStudy.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Application.UseCases.Auth.Login
{
    public class LoginUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUseCase(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
        {
            var user = await userRepository.GetByEmail(request.Email);
            if (user is null)
                throw new KeyNotFoundException(UserError.UserEmailNotFound.Message);
            
            var validPassword =
                _passwordHasher.ValidPassword(
                    request.Password,
                    user.PasswordHash);

            if (!validPassword)
                throw new InvalidDataException(UserError.UserPasswordInvalid.Message);

            var token = await _tokenGenerator.Execute(user);
            return new LoginResponse 
            { 
                Token = token 
            };

        }
    }
}
