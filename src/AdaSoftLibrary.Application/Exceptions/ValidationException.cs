using FluentValidation.Results;

namespace AdaSoftLibrary.Application.Exceptions;

/// <summary>
/// 
/// </summary>
public class ValidationException : ApplicationException
{
    public List<string> ValdationErrors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        ValdationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValdationErrors.Add(validationError.ErrorMessage);
        }
    }

    public void AddCustomError(string errorMessage)
    {
        if (ValdationErrors is null)
            ValdationErrors = new List<string>();

        ValdationErrors.Add(errorMessage);
    }
}
