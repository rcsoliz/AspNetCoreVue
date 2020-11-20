using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.BaseModel;
using Model.Identity;
using Persistence.Database.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CountryConfig(modelBuilder.Entity<Country>());
            new CategoryConfig(modelBuilder.Entity<Category>());
            new ClientConfig(modelBuilder.Entity<Client>());
            new ProductConfig(modelBuilder.Entity<Product>());
            new OrderConfig(modelBuilder.Entity<Order>());
            new OrderDetailConfig(modelBuilder.Entity<OrderDetail>());
            
            new ApplicationUserConfig(modelBuilder.Entity<ApplicationUser>());
            new ApplicationRoleConfig(modelBuilder.Entity<ApplicationRole>());
        }

        public override int SaveChanges()
        {
            MakeAudit();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            MakeAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void MakeAudit()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is Audit
                     && (x.State == EntityState.Added || x.State == EntityState.Modified)
            );

            var now = DateTime.Now;

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as Audit;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }

                entity.UpdatedAt = now;
            }
        }
   
    }

}
