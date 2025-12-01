using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class LoanVerificationRepository : ILoanVerificationRepository
    {
        private readonly ApplicationDbContext _context;
        public LoanVerificationRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<LoanVerification>> GetAllAsync() =>
            await _context.LoanVerifications.Include(l => l.AssignedOfficer).Include(l => l.LoanRequest).ToListAsync();

        public async Task<LoanVerification?> GetByIdAsync(int id) =>
            await _context.LoanVerifications.Include(l => l.AssignedOfficer).Include(l => l.LoanRequest).FirstOrDefaultAsync(l => l.Id == id);

        public async Task<bool> AssignOfficerAsync(int verificationId, int officerId)
        {
            var lv = await _context.LoanVerifications.FindAsync(verificationId);
            if (lv == null) return false;
            lv.AssignedOfficerId = officerId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, LoanVerification model)
        {
            var lv = await _context.LoanVerifications.FindAsync(id);
            if (lv == null) return false;
            lv.Status = model.Status;
            lv.Remarks = model.Remarks;
            lv.AssignedOfficerId = model.AssignedOfficerId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lv = await _context.LoanVerifications.FindAsync(id);
            if (lv == null) return false;
            _context.LoanVerifications.Remove(lv);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
