using System.ComponentModel.DataAnnotations;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Models;

public class PersonRelation
{
    [Range(0, 3)]
    public RelationType RelationType { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; }
    
    public int RelatedPersonId { get; set; }
    public Person RelatedPerson { get; set; }
}