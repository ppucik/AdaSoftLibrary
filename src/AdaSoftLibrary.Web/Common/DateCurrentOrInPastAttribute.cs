using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Common;

#pragma warning disable CS8765, CS8603

/// <summary>
/// Validačný atribút kontroľuje, či je zadaná hodnota aktuálny dátum alebo v minulosti.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DateCurrentOrInPastAttribute : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return "Dátum nie je aktálny alebo v minulosti";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateOnly dateValue = (DateOnly)value;

        if (dateValue > DateOnly.FromDateTime(DateTime.Now))
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        return ValidationResult.Success;
    }
}
