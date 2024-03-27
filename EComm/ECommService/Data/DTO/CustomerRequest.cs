using System.ComponentModel.DataAnnotations;

namespace ECommService.Data.DTO;

public record CustomerRequest(
    [Required] string Name,
    [MinLength(5)] string? PostalCode
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var retVal = new List<ValidationResult>();

        if (Name.StartsWith("X")) {
            retVal.Add(new ValidationResult("Name cannot start with X"));
        }

        return retVal;
    }
}
