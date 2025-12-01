using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Loan_Management_System.ViewModels
{
    public class LoanRequestDto
    {
        [Required]
        [Range(1000, 10000000, ErrorMessage = "Amount must be greater than 0")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Range(1, 120, ErrorMessage = "TenureMonths must be at least 1 month")]
        [JsonPropertyName("tenureMonths")]
        public int TenureMonths { get; set; }

        [Required(ErrorMessage = "Purpose is required")]
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }
    }

}
