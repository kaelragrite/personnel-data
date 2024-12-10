using System.ComponentModel.DataAnnotations;
using PersonnelData.Messages.Dtos;
using PersonnelData.Messages.Validations;
using PersonnelData.Resources;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages;

#pragma warning disable CS8618
// Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class CreatePersonRequest
{
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedResources))]
    [GeoLatinRestriction(ErrorMessageResourceName = "GeoLatinRestriction", ErrorMessageResourceType = typeof(SharedResources))]
    public string Name { get; set; }

    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(SharedResources))]
    [GeoLatinRestriction(ErrorMessageResourceName = "GeoLatinRestriction", ErrorMessageResourceType = typeof(SharedResources))]
    public string Surname { get; set; }
    
    public Gender Gender { get; set; }
    
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "ExactStringLength", ErrorMessageResourceType = typeof(SharedResources))]
    public string IdentificationNumber { get; set; }
    
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
    [MinimumAge(18, ErrorMessageResourceName = "MinimumAge", ErrorMessageResourceType = typeof(SharedResources))]
    public DateTime BirthDate { get; set; }
    
    public int CityId { get; set; }
    
    public List<PhoneDto> Phones { get; set; }
}