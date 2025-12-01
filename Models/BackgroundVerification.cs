using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public class BackgroundVerification
    {
        [Key]
        public int Id { get; set; }
        public int LoanRequestId { get; set; }
        [ForeignKey("LoanRequestId")]
        public LoanRequest LoanRequest { get; set; }


        public string Details { get; set; }
        public string Status { get; set; }
    }
}
