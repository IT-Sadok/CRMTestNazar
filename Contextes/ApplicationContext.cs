using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CRM.Models;
public class ApplicationContext : IdentityDbContext<EmployeeModel, IdentityRole<int>, int>
{
    public DbSet<EmployeeModel> Employees => Set<EmployeeModel>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<EmployeeModel>().ToTable("employees");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("employee_roles");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("employee_claims");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("role_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("employee_logins");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("employee_tokens");
    }
}