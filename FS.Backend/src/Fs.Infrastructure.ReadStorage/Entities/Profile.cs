using System;

namespace Fs.Infrastructure.ReadStorage.Entities
{
    public sealed record Profile
    {
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public DateTime BirthDate { get; init; }
        public string About { get; init; }
        public string Phone { get; init; }
        public Address Address { get; init; }
    }
}
