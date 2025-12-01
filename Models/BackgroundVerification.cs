using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public enum VerificationStatus
    {
        Pending,
        Completed,
        Rejected
    }

    public class BackgroundVerification
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to LoanRequest
        public int LoanRequestId { get; set; }
        [ForeignKey("LoanRequestId")]
        public LoanRequest LoanRequest { get; set; }

        // Assigned Loan Officer
        public int? AssignedOfficerId { get; set; }
        [ForeignKey("AssignedOfficerId")]
        public LoanOfficer? AssignedOfficer { get; set; }

        // Verification details
        [Required]
        public string Details { get; set; } = string.Empty;

        // Verification status
        public VerificationStatus Status { get; set; } = VerificationStatus.Pending;

        // Optional remarks
        public string? Remarks { get; set; }
    }
}
