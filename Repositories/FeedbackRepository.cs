using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        public FeedbackRepository(ApplicationDbContext context) => _context = context;

        public async Task<FeedbackQuestion> AddQuestionAsync(FeedbackQuestion question)
        {
            _context.FeedbackQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<IEnumerable<FeedbackQuestion>> GetQuestionsAsync() =>
            await _context.FeedbackQuestions.AsNoTracking().ToListAsync();

        public async Task<bool> UpdateQuestionAsync(int id, FeedbackQuestion question)
        {
            var existing = await _context.FeedbackQuestions.FindAsync(id);
            if (existing == null) return false;
            existing.Question = question.Question;
            existing.AnswerType = question.AnswerType;
            existing.IsActive = question.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CustomerFeedback>> GetCustomerFeedbackAsync() =>
            await _context.CustomerFeedbacks
                .Include(f => f.Customer)
                .Include(f => f.FeedbackQuestion)
                .AsNoTracking()
                .ToListAsync();
    }
}
