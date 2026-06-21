using SmartStudy.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

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
