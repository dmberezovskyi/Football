using System;
using System.Collections.Generic;
using Fs.Domain.SeedWork;
using Fs.Domain.Values;

namespace Fs.Domain.Aggregates.UserAggregate
{
    public class Profile : ValueObject
    {
        public string FirstName { get; protected set; }
        public string MiddleName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string About { get; protected set; }
        public string Phone { get; protected set; }
        public Address Address { get; protected set; }

        protected Profile() { }
        public Profile(string firstName, string middleName, string lastName, DateTime birthDate, string about, string phone, Address address)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BirthDate = birthDate;
            About = about;
            Phone = phone;
            Address = address;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return MiddleName;
            yield return LastName;
            yield return BirthDate;
            yield return About;
            yield return Phone;
            yield return Address;
        }
    }
}
