using Loan_Management_System.Models;

namespace Loan_Management_System.Repositories
{
    public interface ILoanRepository
    {
        Task<LoanRequest> ApplyLoanAsync(LoanRequest loan);
        Task<IEnumerable<LoanRequest>> GetLoansAsync();
        Task<LoanRequest> GetLoanByIdAsync(int id);
        Task<LoanRequest> UpdateLoanAsync(LoanRequest loan);
    }
}
