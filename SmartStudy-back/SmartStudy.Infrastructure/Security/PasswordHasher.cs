using Azure.Core;
using SmartStudy.Application.Interfaces.Security;
using SmartStudy.Domain.Entities;
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

        public bool ValidPassword(string password, string passwordHash)
        {
            return
                BCrypt.Net.BCrypt.Verify(
                    password,
                    passwordHash);
        }
    }
}
