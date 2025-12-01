using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        public LoanRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<LoanRequest>> GetAllAsync() =>
            await _context.LoanRequests.Include(l => l.Customer).Include(l => l.AssignedOfficer).ToListAsync();

        public async Task<LoanRequest?> GetByIdAsync(int id) =>
            await _context.LoanRequests.Include(l => l.Customer).Include(l => l.AssignedOfficer).FirstOrDefaultAsync(l => l.Id == id);

        public async Task AssignLoanOfficerAsync(int loanRequestId, int officerId)
        {
            var loan = await _context.LoanRequests.FindAsync(loanRequestId);
            if (loan == null) return;
            loan.AssignedOfficerId = officerId;
            await _context.SaveChangesAsync();
        }

        public async Task<LoanRequest> ApplyLoanAsync(LoanRequest loanRequest)
        {
            _context.LoanRequests.Add(loanRequest);
            await _context.SaveChangesAsync();
            return loanRequest;
        }
    }
}
