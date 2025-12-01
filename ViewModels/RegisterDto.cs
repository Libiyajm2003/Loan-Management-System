using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.ViewModels
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // Admin, Customer, Officer
    }
}
