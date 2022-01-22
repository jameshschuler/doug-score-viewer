using System.ComponentModel.DataAnnotations;

namespace DougScoreViewerAPI.Models.Attributes;

public class SortByAttribute : ValidationAttribute
{
    private readonly IEnumerable<string> _sortableProperties = new List<string> { "TotalDougScore", "DailyScore", "WeekendScore", "Year" };
    
    protected override ValidationResult? IsValid(object? value,
        ValidationContext validationContext)
    {
        // var request = (SearchDougScoresRequest)validationContext.ObjectInstance;
        var sortBy = value?.ToString();

        if (!string.IsNullOrWhiteSpace(sortBy) && !_sortableProperties.Contains(sortBy))
        {
            return new ValidationResult($"'{value}' is not a valid sortable property");
        }
        
        return ValidationResult.Success;
    }
}