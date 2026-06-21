using SmartStudy.Application.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Execute(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
