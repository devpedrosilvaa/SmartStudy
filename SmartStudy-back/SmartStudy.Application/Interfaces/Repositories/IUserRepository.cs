using SmartStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmail(string email);
    }
}
