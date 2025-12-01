using Loan_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<BackgroundVerification> BackgroundVerifications { get; set; }
        public DbSet<LoanVerification> LoanVerifications { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<HelpReport> HelpReports { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configure relationships and optional properties
            builder.Entity<ApplicationUser>().HasOne(a => a.CustomerProfile).WithOne(c => c.User).HasForeignKey<Customer>(c => c.ApplicationUserId).IsRequired(false);
            builder.Entity<ApplicationUser>().HasOne(a => a.OfficerProfile).WithOne(o => o.User).HasForeignKey<LoanOfficer>(o => o.ApplicationUserId).IsRequired(false);


            builder.Entity<Customer>().HasMany(c => c.LoanRequests).WithOne(l => l.Customer).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<LoanOfficer>().HasMany(o => o.AssignedLoanRequests).WithOne(l => l.AssignedOfficer).OnDelete(DeleteBehavior.SetNull);
        }
    }
}