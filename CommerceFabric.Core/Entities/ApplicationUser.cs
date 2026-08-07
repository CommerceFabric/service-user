namespace CommerceFabric.Core.Entities
{
    /// <summary>
    /// Define the ApplicationUser class which acts as entity model class used by Dapper (lighter ORM than EF Core) to map to the database table for storing user information.
    /// </summary>
    public class ApplicationUser
    {
        public Guid UserID { get; set; }
        public string? Gender { get; set; }
        public string? Bio { get; set; }
    }
}
