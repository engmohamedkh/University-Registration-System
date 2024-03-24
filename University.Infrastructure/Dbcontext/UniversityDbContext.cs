using University.Core.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using University.Models;

namespace University.Infrastructure.Dbcontext
{
    public class UniversityDbContext : IdentityDbContext<ApplicationUser>
    {
        public UniversityDbContext(DbContextOptions options): base(options){}
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure tables Relations

            //modelBuilder.Entity<Student>().HasMany(c => c.Hotels).WithOne(h => h.Company).OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Department>().HasMany(c => c.Orders).WithOne(o => o.Customer).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasOne(s => s.Department).WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId);

   

        }
    }
}
