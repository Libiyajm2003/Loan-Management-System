using Loan_Management_System.Models;

namespace Loan_Management_System.Repositories
{
    public interface IOfficerRepository
    {
        Task<IEnumerable<LoanRequest>> GetAssignedLoansAsync(string officerId);
        Task<LoanVerification> UpdateLoanVerificationAsync(int loanId, LoanVerification model);
        Task<BackgroundVerification> UpdateBackgroundVerificationAsync(int loanId, BackgroundVerification model);
    }
}