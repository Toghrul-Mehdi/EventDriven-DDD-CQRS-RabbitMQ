namespace ECommerce.Application.DTOs.Product;

public record UpdateProductDto(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string CategoryId);
