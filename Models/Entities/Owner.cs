using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Owner : IUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [Phone]
        [StringLength(40)]
        public string? Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(40)]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public string? BusinessName { get; set; }

        public string? MembershipNumber { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

    }
}
