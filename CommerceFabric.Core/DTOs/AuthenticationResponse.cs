namespace CommerceFabric.Core.DTOs
{
    /// <summary>
    /// The AuthenticationResponse record is used to encapsulate the response data for authentication requests. It includes properties such as UserID, Email, PersonName
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="Email"></param>
    /// <param name="PersonName"></param>
    /// <param name="Gender"></param>
    /// <param name="Token"></param>
    /// <param name="Success"></param>
    public record AuthenticationResponse(
        Guid UserID,
        string? Email,
        string? PersonName,
        string? Gender,
        string? Token,
        bool Success
    );
}
