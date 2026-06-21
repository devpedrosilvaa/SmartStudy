using SmartStudy.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        public User() {}

        public User(
            string name,
            string email,
            string passwordHash
        )
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = Roles.User;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
