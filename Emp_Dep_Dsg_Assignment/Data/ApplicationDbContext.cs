using Emp_Dep_Dsg_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp_Dep_Dsg_Assignment.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpDep>().HasKey(t => new { t.EmployeeID, t.DepartmentID });    //composit PK
            modelBuilder.Entity<EmpDep>()
                .HasOne(t => t.Employee)
                .WithMany(t => t.Employees)
                .HasForeignKey(t => t.EmployeeID);

            modelBuilder.Entity<EmpDep>()
              .HasOne(t => t.Department)
              .WithMany(t => t.EmpDep)
              .HasForeignKey(t => t.DepartmentID);



            /* modelBuilder.Entity<Emp_Dep_Composit>()
                 .HasKey(nameof(Emp_Dep_Composit.DepartmentID), nameof(Emp_Dep_Composit.EmployeeID));*/
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmpDep> EmpDep { get; set; }
    }
}
