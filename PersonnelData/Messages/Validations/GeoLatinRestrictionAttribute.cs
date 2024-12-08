using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using PersonnelData.Resources;

namespace PersonnelData.Messages.Validations;

public class GeoLatinRestrictionAttribute : ValidationAttribute
{
    private const string GeorgianAlphabet = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
    private const string EnglishAlphabet = "abcdefghijqlmnopqrstuvwxyz";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valueString = value?.ToString();

        if (valueString != null && valueString.Any(x => GeorgianAlphabet.Contains(x)) && valueString.Any(x => EnglishAlphabet.Contains(x)))
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}