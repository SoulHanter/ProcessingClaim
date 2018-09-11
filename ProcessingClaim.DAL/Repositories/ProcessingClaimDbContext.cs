using Microsoft.AspNet.Identity.EntityFramework;
using ProcessingClaim.DAL.Logical;
using ProcessingClaim.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Repositories
{
    /// <summary>
    /// Db context базы
    /// </summary>
    public class ProcessingClaimDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Category> Categories { get; set; }   

        public ProcessingClaimDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new AdminDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public static ProcessingClaimDbContext Create()
        {
            return new ProcessingClaimDbContext();
        }
    }
}
