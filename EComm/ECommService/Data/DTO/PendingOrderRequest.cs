using System.ComponentModel.DataAnnotations;

namespace ECommService.Data.DTO;

public record PendingOrderRequest(
    [Required] int CustomerId,
    [Required] int ProductId,
    [Required, Range(1, 999)] int Quantity
);
