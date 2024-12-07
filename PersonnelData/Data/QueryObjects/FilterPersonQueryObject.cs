using PersonnelData.Shared.Enums;

namespace PersonnelData.Data.QueryObjects;

public class FilterPersonQueryObject
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Gender? Gender { get; set; }
    public string? IdentificationNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? CityId { get; set; }

    public int Page { get; set; }
    public int PageCount { get; set; }
}