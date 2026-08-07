using CommerceFabric.Core.Enums;

namespace CommerceFabric.Core.DTOs
{
    public record UpdateUserDetailsRequest(
        Guid UserID,
        string? Gender,
        string? Bio
        );
}