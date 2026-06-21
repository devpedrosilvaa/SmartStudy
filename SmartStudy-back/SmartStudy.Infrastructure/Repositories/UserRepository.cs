using Microsoft.EntityFrameworkCore;
using SmartStudy.Application.Interfaces.Repositories;
using SmartStudy.Domain.Entities;
using SmartStudy.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContextDb _context;

        public UserRepository(ApplicationContextDb context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context
                .Users
                .AddAsync(user);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
