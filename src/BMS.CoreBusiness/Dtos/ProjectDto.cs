namespace BMS.CoreBusiness.Dtos
{
    public readonly record struct ProjectDto(string? Name, string CreatedBy, DateTimeOffset CreatedOnUtc, string LastModifiedBy, DateTimeOffset? LastModifiedOnUtc);
    public readonly record struct ProjectDropdownDto(Guid Id, string Name);
}
