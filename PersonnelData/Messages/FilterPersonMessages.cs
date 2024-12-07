using PersonnelData.Models;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class FilterPersonQuery
{
    public int Page { get; set; }
    public int PageCount { get; set; }
    
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Gender? Gender { get; set; }
    public string? IdentificationNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? CityId { get; set; }
}

public class FilterPersonResponse
{
    public FilterPersonResponse(List<Person> persons) => 
        Persons = new List<FilterPerson>(persons.Select(x => new FilterPerson(x)));

    public List<FilterPerson> Persons { get; }
    
    public class FilterPerson
    {
        public FilterPerson(Person person)
        {
            Name = person.Name;
            Surname = person.Surname;
            Gender = person.Gender;
            IdentificationNumber = person.IdentificationNumber;
            BirthDate = person.BirthDate;
            CityId = person.CityId;
        }
        
        public string Name { get; }
        public string Surname { get; }
        public Gender Gender { get; }
        public string IdentificationNumber { get; }
        public DateTime BirthDate { get; }
        public int CityId { get; }
    }
}