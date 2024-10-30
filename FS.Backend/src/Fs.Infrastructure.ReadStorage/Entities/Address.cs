namespace Fs.Infrastructure.ReadStorage.Entities
{
    public sealed record Address
    {
        public string Country { get; init; }
        public string State { get; init; }
        public string City { get; init; }
        public string ZipCode { get; init; }
        public string StreetAddress { get; init; }
    }
}
