using System.ComponentModel.DataAnnotations;

namespace PersonnelData.Messages.Validations;

public class GeoLatinRestrictionAttribute : ValidationAttribute
{
    private const string GeorgianAlphabet = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
    private const string EnglishAlphabet = "abcdefghijqlmnopqrstuvwxyz";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valueString = value?.ToString();
        
        if (string.IsNullOrWhiteSpace(valueString))
            return new ValidationResult("Value is required.");

        if (valueString.Any(x => GeorgianAlphabet.Contains(x)) && valueString.Any(x => EnglishAlphabet.Contains(x)))
            return new ValidationResult("Value can't contain letters from Georgian and English alphabet at the same time.");

        return ValidationResult.Success;
    }
}