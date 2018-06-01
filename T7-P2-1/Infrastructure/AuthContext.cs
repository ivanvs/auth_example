using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using T7_P2_1.Models;

namespace T7_P2_1.Infrastructure
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext() : base("UserManagmentContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuthContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
        }
    }

}