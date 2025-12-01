using Loan_Management_System.Models;

namespace Loan_Management_System.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<LoanRequest>> GetAllLoanRequestsAsync();
        Task<HelpReport> AddHelpReportAsync(HelpReport report);
        Task<IEnumerable<Feedback>> GetCustomerFeedbackAsync();
    }
}