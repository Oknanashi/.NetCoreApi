using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Persistence
{
    public class DataContext :IdentityDbContext<AppEmployee>
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        
       
        public DbSet<AppUser> AppUsers { get; set; }
       
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // builder.Entity<AppEmployee>(x => x.HasKey(ua => new {  ua.CompanyId }));
            
            builder.Entity<Company>()
            .HasMany(c=>c.AppUsers)
            .WithOne(e=>e.EmployeeCompany);

            // builder.Entity<AppEmployee>()
            // .HasOne(a => a.EmployeeCompany)
            // .WithMany(u => u.Employees)
            // .HasForeignKey(a => a.AppEmployeeId);
            
        }
    }
}
