using Microsoft.AspNetCore.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public class LoanOfficer
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }
        public string Region { get; set; }
        public bool IsApproved { get; set; }


        // Navigation
        public ICollection<LoanRequest> AssignedLoanRequests { get; set; }
        public ICollection<BackgroundVerification> BackgroundVerifications { get; set; }
    }
}
