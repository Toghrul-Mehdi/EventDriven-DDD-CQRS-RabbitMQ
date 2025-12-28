namespace ECommerce.SharedKernel.Domain;

public record ValidationError(string PropertyName, string ErrorMessage);