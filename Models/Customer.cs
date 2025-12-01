using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Loan_Management_System.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public string Address { get; set; }

        // Make PAN and Aadhaar nullable to avoid insertion errors
        public string? PAN { get; set; }
        public string? Aadhaar { get; set; }

        // Navigation properties
        public ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
