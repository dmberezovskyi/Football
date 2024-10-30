using System;
using System.Collections.Generic;
using System.Text;
using Fs.Domain.SeedWork;

namespace Fs.Domain.Values
{
    public class Address : ValueObject
    {
        public string Country { get; protected set; }
        public string State { get; protected set; }
        public string City { get; protected set; }
        public string ZipCode { get; protected set; }
        public string StreetAddress { get; protected set; }

        protected Address() { }

        public Address(string country, string state, string city, string zipcode, string streetAddress)
        {   
            Country = country;
            State = state;
            City = city;
            ZipCode = zipcode;
            StreetAddress = streetAddress;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {   
            yield return Country;
            yield return State;
            yield return City;
            yield return ZipCode;
            yield return StreetAddress;
        }
    }
}
