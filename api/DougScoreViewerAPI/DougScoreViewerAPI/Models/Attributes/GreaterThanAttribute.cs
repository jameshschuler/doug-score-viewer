using System.ComponentModel.DataAnnotations;
using DougScoreViewerAPI.Models.Request;

namespace DougScoreViewerAPI.Models.Attributes;

public class GreaterThanAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var request = (SearchDougScoresRequest)validationContext.ObjectInstance;
        
        if (value is null || request.MinYear is null)
        {
            return ValidationResult.Success;
        }

        return (int)value < request.MinYear ? 
            new ValidationResult($"Max. Year ({value}) cannot be before the Min. Year ({request.MinYear}).") : ValidationResult.Success;
    }
}