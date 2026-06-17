using eCommerce.Core.Enums;

namespace eCommerce.Core.DTOs
{
    /// <summary>
    /// Define the RegisterRequest record which is used to encapsulate the registration request data, including email and password.
    /// Made as a record instead of a class to leverage immutability and value-based equality, which is suitable for data transfer objects (DTOs).
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record RegisterRequest(
        string? Email,
        string? Password,
        string? PersonName,
        GenderOptions Gender
        );
}
