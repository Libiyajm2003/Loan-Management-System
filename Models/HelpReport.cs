using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System.Models
{
    public class HelpReport
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
