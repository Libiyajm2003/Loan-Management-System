using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Loan_Management_System.Models
{
    public class LoanOfficer
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? User { get; set; }
        public string Region { get; set; }
        public bool IsApproved { get; set; }

        public string Status { get; set; } = "Pending";

        // Navigation
        public ICollection<LoanRequest> AssignedLoanRequests { get; set; } = new List<LoanRequest>();
        public ICollection<BackgroundVerification> AssignedBackgroundVerifications { get; set; } = new List<BackgroundVerification>();
        public ICollection<LoanVerification> AssignedLoanVerifications { get; set; } = new List<LoanVerification>();
    }
}
