using System.ComponentModel.DataAnnotations;
using DougScoreViewerAPI.Models.Request;

namespace DougScoreViewerAPI.Models.Attributes;

public class GreaterThanAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var request = (SearchDougScoresRequest)validationContext.ObjectInstance;
        
        if (value is null || request.StartYear is null)
        {
            return ValidationResult.Success;
        }

        return (int)value < request.StartYear ? 
            new ValidationResult($"End Year ({value}) cannot be before Start Year ({request.StartYear}).") : ValidationResult.Success;
    }
}