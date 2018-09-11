using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProcessingClaim.DAL.Models;
using Claim = ProcessingClaim.DAL.Models.Claim;
using ProcessingClaim.DAL.Logical;

namespace ProcessingClaim.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}