using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.API.Models
{
    
        public class Role
        {
            [Key]
            public int Id { get; set; }
            public required string RoleName { get; set; }
            public string? Description { get; set; }
        }

        public class User
        {
            [Key]
            public int Id { get; set; }
            public required string Username { get; set; }
            public required string PasswordHash { get; set; }
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public bool IsActive { get; set; } = true;

            public int RoleId { get; set; }
            [ForeignKey("RoleId")]
            public virtual Role? Role { get; set; }
        }

        public class Partner
        {
            [Key]
            public int Id { get; set; }
            public required string Name { get; set; }
            public required string Type { get; set; } // 'Customer' or 'Supplier'
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public string? TaxCode { get; set; }
            public decimal CurrentBalance { get; set; } = 0;

            public int? AssignedUserId { get; set; }
            [ForeignKey("AssignedUserId")]
            public virtual User? AssignedUser { get; set; }
        }
    
}
