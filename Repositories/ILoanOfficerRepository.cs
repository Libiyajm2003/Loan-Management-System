using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface ILoanOfficerRepository
    {
        Task<IEnumerable<LoanOfficer>> GetAllAsync();
        Task<LoanOfficer?> GetByIdAsync(int id);
        Task<bool> ApproveLoanOfficerAsync(int id);
        Task<bool> RejectLoanOfficerAsync(int id);
    }
}
