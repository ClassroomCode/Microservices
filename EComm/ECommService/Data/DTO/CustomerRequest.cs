using System.ComponentModel.DataAnnotations;

namespace ECommService.Data.DTO;

public record CustomerRequest(
    [Required] string Name,
    [MinLength(5)] string? PostalCode
);
