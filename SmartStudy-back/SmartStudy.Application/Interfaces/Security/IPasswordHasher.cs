using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string Execute(string password);
        bool ValidPassword(string password, string passwordHash);
    }
}
