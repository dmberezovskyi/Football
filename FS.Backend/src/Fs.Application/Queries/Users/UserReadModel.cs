using System;
using Fs.Infrastructure.ReadStorage.Entities;

namespace Fs.Application.Queries.Users
{
    public sealed record UserReadModel
    {
        public Guid Id { get; init; }
        public int Version { get; init; }

        public SnippetModel Snippet { get; init; }
        public ProfileModel Profile { get; init; }
        public TeamModel Team { get; init; }
        public OrganizationModel Organization { get; init; }
        public UserInfoModel UserInfo { get; init; }

        public UserInfoDetailedModel UserInfoDetailed { get; init; }

        public sealed record SnippetModel
        {
            public string FirstName { get; init; }
            public string MiddleName { get; init; }
            public string LastName { get; init; }
            public DateTime BirthDate { get; init; }
            public string Role { get; init; }
        }
        public sealed record ProfileModel
        {
            public string FirstName { get; init; }
            public string MiddleName { get; init; }
            public string LastName { get; init; }
            public string Email { get; init; }
            public DateTime BirthDate { get; init; }
            public string About { get; init; }
            public string Phone { get; init; }
            public AddressReadModel Address { get; init; }

        }
        public sealed record TeamModel
        {
            public Guid Id { get; init; }
            public string Name { get; init; }
        }
        public sealed record OrganizationModel
        {
            public Guid Id { get; init; }
            public string Name { get; init; }
        }

        public sealed record UserInfoModel
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }
            public string Email { get; set; }
            public string Role { get; init; }
        }

        public sealed record UserInfoDetailedModel
        {
            public UserStatus Status { get; set; }

            public DateTime CreatedOn { get; set; }

            public DateTime UpdatedOn { get; set; }

            public bool EmailConfirmed { get; set; }
        }
    }
}