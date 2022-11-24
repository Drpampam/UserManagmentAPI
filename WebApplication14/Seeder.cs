using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;

namespace WebApplication14
{
    public static class Seeder
    {
        public static  void seeding(AppIdentityDbContext appIdentityDbContext)
        {
            appIdentityDbContext.Database.EnsureCreated();

            var addresses = new Address() { Name = "lagos" };
            appIdentityDbContext.Addresses.Add(addresses);
            appIdentityDbContext.SaveChanges();
        }
    }
}
