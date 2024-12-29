using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }    

        [Required]
        [StringLength(40)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Cal { get; set; }

        [Required]
        public string? MembershipNumber { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
