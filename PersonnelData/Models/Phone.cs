using System.ComponentModel.DataAnnotations;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Models;

public class Phone
{
    public int Id { get; set; }
    
    [Range(0, 2)]
    public PhoneType PhoneType { get; set; }
    
    [MinLength(4)]
    [MaxLength(50)]
    public string Number { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; }
}