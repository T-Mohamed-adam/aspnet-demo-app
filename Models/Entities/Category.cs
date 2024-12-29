using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(40)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        [Required]
        public string? MembershipNumber { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; }



    }
}
