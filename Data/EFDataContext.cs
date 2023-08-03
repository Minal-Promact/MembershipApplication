using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MembershipApplication.Data
{
    //public class EFDataContext : IdentityDbContext<ApplicationUser>
    public class EFDataContext : IdentityDbContext<ApplicationUser,ApplicationRole,Int64>
    {        
        public EFDataContext(DbContextOptions<EFDataContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           

        }

    }
}
