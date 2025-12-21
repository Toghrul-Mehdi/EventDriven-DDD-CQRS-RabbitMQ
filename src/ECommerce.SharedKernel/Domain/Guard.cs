namespace ECommerce.SharedKernel.Domain;

public static class Guard
{
    
    public static void AgainstNull(object value, string parameterName)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null");
    }

    
    public static void AgainstNullOrEmpty(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{parameterName} cannot be null or empty", parameterName);
    }

    
    public static void AgainstNegativeOrZero(decimal value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentException($"{parameterName} must be greater than zero", parameterName);
    }

    
    public static void AgainstOutOfRange(int value, int min, int max, string parameterName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(parameterName,
                $"{parameterName} must be between {min} and {max}");
    }
}
