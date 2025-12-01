using Loan_Management_System.Models;

namespace Loan_Management_System.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> ApproveCustomerAsync(int customerId);
        Task<bool> RejectCustomerAsync(int customerId);

        Task<IEnumerable<LoanOfficer>> GetAllLoanOfficersAsync();
        Task<bool> ApproveLoanOfficerAsync(int officerId);
        Task<bool> RejectLoanOfficerAsync(int officerId);
        Task<bool> AssignOfficerToBackgroundVerification(int verificationId, int officerId);
        Task<bool> AssignOfficerToLoanVerification(int verificationId, int officerId);

        Task<IEnumerable<LoanRequest>> GetAllLoanRequestsAsync();

        Task<IEnumerable<BackgroundVerification>> GetAllBackgroundVerificationsAsync();
        Task<bool> UpdateBackgroundVerificationAsync(BackgroundVerification verification);
        Task<bool> DeleteBackgroundVerificationAsync(int id);

        Task<IEnumerable<LoanVerification>> GetAllLoanVerificationsAsync();
        Task<bool> UpdateLoanVerificationAsync(LoanVerification verification);
        Task<bool> DeleteLoanVerificationAsync(int id);

        Task<HelpReport> AddHelpReportAsync(HelpReport report);
        Task<IEnumerable<HelpReport>> GetAllHelpReportsAsync();
        Task<bool> UpdateHelpReportAsync(HelpReport report);

        Task<FeedbackQuestion> AddFeedbackQuestionAsync(FeedbackQuestion question);
        Task<IEnumerable<FeedbackQuestion>> GetAllFeedbackQuestionsAsync();
        Task<bool> UpdateFeedbackQuestionAsync(FeedbackQuestion question);
        Task<IEnumerable<CustomerFeedback>> GetCustomerFeedbackAsync();
    }
}