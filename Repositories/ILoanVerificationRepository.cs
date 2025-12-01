using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface ILoanVerificationRepository
    {
        Task<IEnumerable<LoanVerification>> GetAllAsync();
        Task<LoanVerification?> GetByIdAsync(int id);
        Task<bool> AssignOfficerAsync(int verificationId, int officerId);
        Task<bool> UpdateAsync(int id, LoanVerification model);
        Task<bool> DeleteAsync(int id);
    }
}
