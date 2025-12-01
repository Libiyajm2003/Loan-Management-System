using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanRequest>> GetAllAsync();
        Task<LoanRequest?> GetByIdAsync(int id);
        Task AssignLoanOfficerAsync(int loanRequestId, int officerId);
        Task<LoanRequest> ApplyLoanAsync(LoanRequest loanRequest);
    }
}
