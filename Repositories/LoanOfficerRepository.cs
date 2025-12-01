using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class LoanOfficerRepository : ILoanOfficerRepository
    {
        private readonly ApplicationDbContext _context;
        public LoanOfficerRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<LoanOfficer>> GetAllAsync() => await _context.LoanOfficers.ToListAsync();
        public async Task<LoanOfficer?> GetByIdAsync(int id) => await _context.LoanOfficers.FindAsync(id);

        public async Task<bool> ApproveLoanOfficerAsync(int id)
        {
            var officer = await _context.LoanOfficers.FindAsync(id);
            if (officer == null) return false;
            officer.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectLoanOfficerAsync(int id)
        {
            var officer = await _context.LoanOfficers.FindAsync(id);
            if (officer == null) return false;
            officer.IsApproved = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
