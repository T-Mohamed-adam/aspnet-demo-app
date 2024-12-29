using System.Text.Json.Serialization;

namespace EmployeeAdminPortal.Models.Entities
{
    public class Department
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }


        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
