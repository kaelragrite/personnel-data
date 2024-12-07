using System.ComponentModel.DataAnnotations;

namespace PersonnelData.Messages.Validations;

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
        
        ErrorMessage = $"Field value must be at least {_minimumAge} years old.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Field is required.");

        if (value is DateTime date)
        {
            var age = CalculateAge(date);
            if (age < _minimumAge)
                return new ValidationResult(ErrorMessage);
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