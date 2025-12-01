using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.ViewModels
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
