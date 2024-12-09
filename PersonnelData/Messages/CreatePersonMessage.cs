using System.ComponentModel.DataAnnotations;
using PersonnelData.Messages.Dtos;
using PersonnelData.Messages.Validations;
using PersonnelData.Resources;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

public class CreatePersonRequest
{
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedResources))]
    [GeoLatinRestriction(ErrorMessageResourceName = "GeoLatinRestriction", ErrorMessageResourceType = typeof(SharedResources))]
    public required string Name { get; set; }
    
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedResources))]
    [GeoLatinRestriction(ErrorMessageResourceName = "GeoLatinRestriction", ErrorMessageResourceType = typeof(SharedResources))]
    public required string Surname { get; set; }
    
    public required Gender Gender { get; set; }
    
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "ExactStringLength", ErrorMessageResourceType = typeof(SharedResources))]
    public required string IdentificationNumber { get; set; }
    
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [MinimumAge(18, ErrorMessageResourceName = "MinimumAge", ErrorMessageResourceType = typeof(SharedResources))]
    public required DateTime BirthDate { get; set; }
    
    public required int CityId { get; set; }
    
    public required List<PhoneDto> Phones { get; set; }
}