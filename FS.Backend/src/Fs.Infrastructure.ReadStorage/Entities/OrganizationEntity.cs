namespace Fs.Infrastructure.ReadStorage.Entities
{
    public sealed record OrganizationEntity
        : BaseEntity
    {
        public string Name { get; init; }
        public Address Address { get; init; }
    }
}
