using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public Guid? DepartmentID { get; set; }

        [StringLength(20)]
        public required string Name { get; set; }

        [EmailAddress]
        [StringLength(40)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(10)]
        public string? Phone { get; set; }

        public decimal Salary { get; set; }
       
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;


        [ForeignKey("DepartmentID")]
        public virtual Department? Department { get; set; }

        // New field for the profile picture
        public string? ProfilePicturePath { get; set; }
    }
}
