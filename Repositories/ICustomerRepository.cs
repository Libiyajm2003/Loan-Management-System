using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByUserIdAsync(string userId);
        Task<bool> ApproveCustomerAsync(int id);
        Task<bool> RejectCustomerAsync(int id);
    }
}
