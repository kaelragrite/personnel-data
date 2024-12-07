using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class CreatePersonRelationRequest
{
    public RelationType RelationType { get; set; }
    public int PersonId { get; set; }
    public int RelatedPersonId { get; set; }
}