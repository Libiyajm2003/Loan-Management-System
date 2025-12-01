using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to ApplicationUser
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? User { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        // For Admin approval/rejection
        public bool IsApproved { get; set; } = false;

        // Navigation
        public virtual ICollection<LoanRequest>? LoanRequests { get; set; }
        public virtual ICollection<CustomerFeedback>? CustomerFeedbacks { get; set; }
    }
}
