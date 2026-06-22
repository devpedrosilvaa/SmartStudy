using SmartStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Application.Interfaces.Security
{
    public interface ITokenGenerator
    {
        Task<string> Execute(User user);
    }
}
