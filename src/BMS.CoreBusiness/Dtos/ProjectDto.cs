namespace BMS.CoreBusiness.Dtos
{
    public readonly record struct ProjectDto(Guid Id, string? Name, Guid CreatedById, DateTimeOffset CreatedOnUtc, Guid LastModifiedById, DateTimeOffset? LastModifiedOnUtc, string? IPAddress);
    public readonly record struct ProjectDropdownDto(Guid Id, string Name);
}
