namespace CommerceFabric.Core.DTOs
{
    /// <summary>
    /// Define the UserDTO record which is used to encapsulate the user data, including user ID, email, person name
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="Email"></param>
    /// <param name="PersonName"></param>
    /// <param name="Gender"></param>
    /// <param name="Surname"></param>
    public record UserDTO(
        Guid UserID,
        string? Email,
        string? PersonName,
        string? Gender,
        string? Surname,
        string? Bio
    );
}
