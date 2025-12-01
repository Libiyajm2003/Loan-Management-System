using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }


        // Role will still be handled by Identity roles; convenience field (optional)
        public string Role { get; set; }


        // Navigation
        public Customer CustomerProfile { get; set; }
        public LoanOfficer OfficerProfile { get; set; }
    }
}
