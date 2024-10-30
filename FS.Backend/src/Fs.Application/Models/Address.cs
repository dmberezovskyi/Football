namespace Fs.Application.Models
{
    public sealed record Address
    {
        public string Country { get; init; }
        public string State { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public string StreetAddress { get; init; }

        public static implicit operator Domain.Values.Address(Address address)
        {
            if (address == null)
                return null;
            return new Domain.Values.Address(
                address.Country,
                address.State,
                address.City,
                address.ZipCode,
                address.StreetAddress);
        }
    }
}
