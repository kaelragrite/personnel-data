using System.ComponentModel.DataAnnotations;

namespace PersonnelData.Models;

public class City
{
    public int Id { get; set; }
    
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; }
}