namespace EmployeeAdminPortal.Models.Entities
{
    public interface IUser
    {
        Guid Id { get; }
        string Email { get; }
        string Password { get; }
        string MembershipNumber { get; }
    }

}
