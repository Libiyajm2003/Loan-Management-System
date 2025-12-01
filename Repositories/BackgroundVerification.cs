using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class BackgroundVerificationRepository : IBackgroundVerificationRepository
    {
        private readonly ApplicationDbContext _context;
        public BackgroundVerificationRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<BackgroundVerification>> GetAllAsync() =>
            await _context.BackgroundVerifications.Include(b => b.AssignedOfficer).Include(b => b.LoanRequest).ToListAsync();

        public async Task<BackgroundVerification?> GetByIdAsync(int id) =>
            await _context.BackgroundVerifications.Include(b => b.AssignedOfficer).Include(b => b.LoanRequest).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<bool> AssignOfficerAsync(int verificationId, int officerId)
        {
            var bv = await _context.BackgroundVerifications.FindAsync(verificationId);
            if (bv == null) return false;
            bv.AssignedOfficerId = officerId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, BackgroundVerification model)
        {
            var bv = await _context.BackgroundVerifications.FindAsync(id);
            if (bv == null) return false;
            bv.Status = model.Status;
            bv.Remarks = model.Remarks;
            bv.Details = model.Details;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bv = await _context.BackgroundVerifications.FindAsync(id);
            if (bv == null) return false;
            _context.BackgroundVerifications.Remove(bv);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
