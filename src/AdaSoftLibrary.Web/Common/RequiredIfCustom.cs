using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Web.Common;

#pragma warning disable CS8765, CS8603

/// <summary>
/// Validačný atribút kontroľuje, či je splnená podmienke pre požadovanú hodnotu
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class RequiredIfCustom(string otherProperty, object targetValue) : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherPropertyValue = validationContext.ObjectType
            .GetProperty(otherProperty)?
            .GetValue(validationContext.ObjectInstance);

        if (otherPropertyValue is null || !otherPropertyValue.Equals(targetValue)) return ValidationResult.Success;

        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult(ErrorMessage ?? "Položka je povinná.");
        }

        return ValidationResult.Success;
    }
}
