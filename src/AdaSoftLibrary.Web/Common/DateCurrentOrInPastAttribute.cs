using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Common;

/// <summary>
/// Aktuálny dátum alebo v minulosti.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DateCurrentOrInPastAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateOnly dateValue = (DateOnly)value;

        if (dateValue > DateOnly.FromDateTime(DateTime.Now))
            return new ValidationResult("Dátum nie je aktálny alebo v minulosti");

        return ValidationResult.Success;
    }
}
