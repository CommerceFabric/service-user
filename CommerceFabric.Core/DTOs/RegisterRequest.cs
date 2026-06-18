using CommerceFabric.Core.Enums;

namespace CommerceFabric.Core.DTOs
{
    /// <summary>
    /// Define the RegisterRequest record which is used to encapsulate the registration request data, including email and password.
    /// Made as a record instead of a class to leverage immutability and value-based equality, which is suitable for data transfer objects (DTOs).
    /// </summary>
    /// <param name="Email"/>
    /// <param name="Password"/>
    /// <param name="PersonName"/>
    /// <param name="Gender"/>
    public record RegisterRequest(
        string Email,
        string Password,
        string PersonName,
        string Gender
        );
}
