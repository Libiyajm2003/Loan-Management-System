using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface IFeedbackRepository
    {
        Task<FeedbackQuestion> AddQuestionAsync(FeedbackQuestion question);
        Task<IEnumerable<FeedbackQuestion>> GetQuestionsAsync();
        Task<bool> UpdateQuestionAsync(int id, FeedbackQuestion question);
        Task<IEnumerable<CustomerFeedback>> GetCustomerFeedbackAsync();
    }
}
