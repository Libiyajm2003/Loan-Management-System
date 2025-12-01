using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;


namespace Loan_Management_System.Repositories
{
    public class OfficerRepository : IOfficerRepository
    {
        private readonly ApplicationDbContext _context;
        public OfficerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanRequest>> GetAssignedLoansAsync(string officerId)
        {
            var officer = await _context.LoanOfficers.FirstOrDefaultAsync(o => o.ApplicationUserId == officerId);
            if (officer == null) return new List<LoanRequest>();

            return await _context.LoanRequests
                .Where(l => l.AssignedOfficerId == officer.Id)
                .Include(l => l.Customer)
                .ToListAsync();
        }

        public async Task<LoanVerification> UpdateLoanVerificationAsync(int loanId, LoanVerification model)
        {
            model.LoanRequestId = loanId;
            _context.LoanVerifications.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<BackgroundVerification> UpdateBackgroundVerificationAsync(int loanId, BackgroundVerification model)
        {
            model.LoanRequestId = loanId;
            _context.BackgroundVerifications.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}