namespace BMS.CoreBusiness.Dtos
{
    public sealed record ProjectDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid CreatedById { get; init; }
        public DateTimeOffset CreatedOnUtc { get; init; }
        public Guid LastModifiedById { get; init; }
        public DateTimeOffset? LastModifiedOnUtc { get; init; }
        public string? IPAddress { get; init; }
    }

    public readonly record struct ProjectDropdownDto(Guid Id, string Name);
}
