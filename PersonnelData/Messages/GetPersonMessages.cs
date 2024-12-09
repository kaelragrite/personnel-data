using PersonnelData.Messages.Dtos;
using PersonnelData.Models;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class GetPersonResponse
{
    public GetPersonResponse(Person person, string? imageBase64String)
    {
        Name = person.Name;
        Surname = person.Surname;
        Gender = person.Gender;
        IdentificationNumber = person.IdentificationNumber;
        BirthDate = person.BirthDate;
        ImageBase64String = imageBase64String;
        CityId = person.CityId;
        Phones = new List<PhoneDto>(person.Phones.Select(x => new PhoneDto
        {
            PhoneType = x.PhoneType,
            Number = x.Number
        }));
        Relations = new List<PersonRelationDto>(person.Relations.Select(x => new PersonRelationDto
        {
            RelationType = x.RelationType,
            RelatedPersonId = x.RelatedPersonId
        }));
    }
    
    public string Name { get; }
    public string Surname { get; }
    public Gender Gender { get; }
    public string IdentificationNumber { get; }
    public DateTime BirthDate { get; }
    public string? ImageBase64String { get; }
    public int CityId { get; }
    public List<PhoneDto> Phones { get; }
    public List<PersonRelationDto> Relations { get; }
}