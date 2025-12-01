using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.Models
{
    public class CustomerFeedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        [Required]
        public int FeedbackQuestionId { get; set; }

        [ForeignKey(nameof(FeedbackQuestionId))]
        public FeedbackQuestion? FeedbackQuestion { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Answer { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
