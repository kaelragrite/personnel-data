using PersonnelData.Messages.Dtos;
using PersonnelData.Models;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class GetPersonResponse
{
    public GetPersonResponse(Person person, byte[]? imageData)
    {
        Name = person.Name;
        Surname = person.Surname;
        Gender = person.Gender;
        IdentificationNumber = person.IdentificationNumber;
        BirthDate = person.BirthDate;
        ImageData = imageData;
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
    public byte[]? ImageData { get; }
    public int CityId { get; }
    public List<PhoneDto> Phones { get; }
    public List<PersonRelationDto> Relations { get; }
}