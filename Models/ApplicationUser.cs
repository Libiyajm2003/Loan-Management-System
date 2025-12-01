using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string? Role { get; set; }

        // Navigation properties
        public Customer? CustomerProfile { get; set; }
        public LoanOfficer? OfficerProfile { get; set; }
    }
}
