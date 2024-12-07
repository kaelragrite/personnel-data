using System.ComponentModel.DataAnnotations;
using PersonnelData.Messages.Dtos;
using PersonnelData.Messages.Validations;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class CreatePersonRequest
{
    [Required(ErrorMessage = "Field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Field length must be between 2-50 symbols.")]
    [GeoLatinRestriction]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Field is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Field length must be between 2-50 symbols.")]
    [GeoLatinRestriction]
    public string Surname { get; set; }
    
    public Gender Gender { get; set; }
    
    [Required(ErrorMessage = "Field is required.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Field length must be exactly 11 symbols.")]
    public string IdentificationNumber { get; set; }
    
    [MinimumAge(18)]
    public DateTime BirthDate { get; set; }
    
    public int CityId { get; set; }
    
    public List<PhoneDto> Phones { get; set; }
}