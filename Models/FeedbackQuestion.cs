using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.Models
{
    public enum FeedbackAnswerType
    {
        Text,
        Rating,
        Boolean
    }

    public class FeedbackQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Question { get; set; } = string.Empty;

        [Required]
        public FeedbackAnswerType AnswerType { get; set; } = FeedbackAnswerType.Text;

        public bool IsActive { get; set; } = true;

        public ICollection<CustomerFeedback> CustomerFeedbacks { get; set; } = new List<CustomerFeedback>();
    }
}

