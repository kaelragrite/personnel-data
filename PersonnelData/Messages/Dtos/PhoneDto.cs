using System.ComponentModel.DataAnnotations;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Messages.Dtos;

public class PhoneDto
{
    public PhoneType PhoneType { get; set; }
    
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Field length must be between 4-50 symbols.")]
    public string Number { get; set; }
}