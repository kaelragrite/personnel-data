using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class DeletePersonRelationRequest
{
    public RelationType RelationType { get; set; }
    public int PersonId { get; set; }
    public int RelatedPersonId { get; set; }
}