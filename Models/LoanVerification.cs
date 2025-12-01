using Loan_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LoanVerification
{
    [Key]
    public int Id { get; set; }

    public int LoanRequestId { get; set; }
    [ForeignKey("LoanRequestId")]
    public LoanRequest LoanRequest { get; set; }

    public int? AssignedOfficerId { get; set; }
    [ForeignKey("AssignedOfficerId")]
    public LoanOfficer? AssignedOfficer { get; set; }

    public string Status { get; set; }
    public string? Remarks { get; set; }
}
