using System;

namespace Fs.Infrastructure.ReadStorage.Entities
{
    public sealed record UserEntity
        : BaseEntity
    {
        public string Email { get; init; }

        public string PasswordHash { get; init; }
        public string PasswordSalt { get; init; }
        
        public UserStatus Status { get; init; }
        public UserRole Role { get; init; }

        public Profile Profile { get; init; }
        public TeamEntity Team { get; init; }
        public OrganizationEntity Organization { get; init; }
    }
}
