using Loan_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class LoanRequest
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("tenureMonths")]
    public int TenureMonths { get; set; }

    [Required]
    [JsonPropertyName("purpose")]
    public string Purpose { get; set; }

    public LoanStatus Status { get; set; } = LoanStatus.Draft;

    public int? AssignedOfficerId { get; set; }

    [ForeignKey("AssignedOfficerId")]
    [JsonIgnore]
    public virtual LoanOfficer? AssignedOfficer { get; set; }

    [JsonIgnore]
    public virtual LoanVerification? LoanVerification { get; set; }

    [JsonIgnore]
    public virtual BackgroundVerification? BackgroundVerification { get; set; }
}
