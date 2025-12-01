using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;


namespace Loan_Management_System.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRequest> ApplyLoanAsync(LoanRequest loan)
        {
            _context.LoanRequests.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<IEnumerable<LoanRequest>> GetLoansAsync()
        {
            return await _context.LoanRequests
                .Include(l => l.Customer)
                .ToListAsync();
        }

        public async Task<LoanRequest> GetLoanByIdAsync(int id)
        {
            return await _context.LoanRequests.FindAsync(id);
        }

        public async Task<LoanRequest> UpdateLoanAsync(LoanRequest loan)
        {
            _context.LoanRequests.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
    }
}