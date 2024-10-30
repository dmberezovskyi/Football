namespace Fs.Application.Queries
{
    public sealed record AddressReadModel
    {
        public string Country { get; init; }
        public string State { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public string StreetAddress { get; init; }
    }
}
