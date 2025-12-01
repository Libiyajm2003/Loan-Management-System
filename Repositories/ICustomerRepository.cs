using Loan_Management_System.Models;

namespace Loan_Management_System.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByUserIdAsync(string userId);
        Task<IEnumerable<LoanRequest>> GetCustomerLoansAsync(int customerId);
        Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
