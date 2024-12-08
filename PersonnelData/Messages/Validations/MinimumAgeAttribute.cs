using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using PersonnelData.Resources;

namespace PersonnelData.Messages.Validations;

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            var age = CalculateAge(date);
            if (age < _minimumAge)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }

    private int CalculateAge(DateTime date)
    {
        var today = DateTime.Today;
        
        var age = today.Year - date.Year;
        if (date.Date > today.AddYears(-age))
            age--;
        
        return age;
    }
}