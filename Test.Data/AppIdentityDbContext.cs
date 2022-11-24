using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Test.Data
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Address>().HasData
                (
                new Address()
                {
                    Id = 1,
                    Name = "Asajon"
                }
                
                );

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Address> Addresses { get; set; }

    }
}
