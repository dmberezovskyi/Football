namespace Fs.Infrastructure.ReadStorage.Entities
{
    public sealed record TeamEntity
        : BaseEntity
    {
        public string Name { get; init; }
        public OrganizationEntity Organization { get; init; }
    }
}
