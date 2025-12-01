using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;


namespace Loan_Management_System.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByUserIdAsync(string userId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }

        public async Task<IEnumerable<LoanRequest>> GetCustomerLoansAsync(int customerId)
        {
            return await _context.LoanRequests
                .Where(l => l.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}