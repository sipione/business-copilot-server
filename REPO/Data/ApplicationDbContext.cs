using Microsoft.EntityFrameworkCore;
using APP.Entities;

namespace REPO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Parameterless constructor for design-time services
        public ApplicationDbContext() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=../REPO/BusinessCopilot.sqlite", b => b.MigrationsAssembly("REPO"))
            .Options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Stakeholder> Stakeholders { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<IncomeCashFlow> IncomeCashFlows { get; set; }
        public DbSet<ExpenseCashFlow> ExpenseCashFlows { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<SubAccounts> SubAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=../REPO/BusinessCopilot.sqlite", b => b.MigrationsAssembly("REPO"));
            }
        }
    }
}