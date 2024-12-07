using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages.Dtos;

public class PersonRelationDto
{
    public RelationType RelationType { get; set; }
    public int RelatedPersonId { get; set; }
}