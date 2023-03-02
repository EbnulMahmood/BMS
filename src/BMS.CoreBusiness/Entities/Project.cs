namespace BMS.CoreBusiness.Entities
{
    public sealed class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ModificationDate { get; set; }
    }
}
