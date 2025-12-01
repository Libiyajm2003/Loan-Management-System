using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface IBackgroundVerificationRepository
    {
        Task<IEnumerable<BackgroundVerification>> GetAllAsync();
        Task<BackgroundVerification?> GetByIdAsync(int id);
        Task<bool> AssignOfficerAsync(int verificationId, int officerId);
        Task<bool> UpdateAsync(int id, BackgroundVerification model);
        Task<bool> DeleteAsync(int id);
    }
}
