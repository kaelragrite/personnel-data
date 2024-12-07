using System.ComponentModel.DataAnnotations;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Models;

public class Person
{
    public int Id { get; set; }
    
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MinLength(2)]
    [MaxLength(50)]
    public string Surname { get; set; }
    
    [Range(0, 1)]
    public Gender Gender { get; set; }
    
    [MinLength(11)]
    [MaxLength(11)]
    public string IdentificationNumber { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    [MaxLength(100)]
    public string? ImageName { get; set; }
    
    public int CityId { get; set; }
    public City City { get; set; }
    
    public List<Phone> Phones { get; set; }
    
    public List<PersonRelation> Relations { get; set; }
}