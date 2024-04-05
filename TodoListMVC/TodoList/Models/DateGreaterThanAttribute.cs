using System.ComponentModel.DataAnnotations;

public class DateGreaterThanAttribute: ValidationAttribute
{
    private readonly string _comparisonProperty;

    // Set the name of the property to compare
    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    // Validate the date comparison
    protected override ValidationResult IsValid(object? value,
        ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        var currentValue = (DateTime)value;

        var comparisonValue = (DateTime)validationContext.
            ObjectType.GetProperty(_comparisonProperty).
            GetValue(validationContext.ObjectInstance);

        if (currentValue < comparisonValue)
        {
            return new ValidationResult(
                ErrorMessage = "Due date must be later than planned date");
        }

        return ValidationResult.Success;
    }
}