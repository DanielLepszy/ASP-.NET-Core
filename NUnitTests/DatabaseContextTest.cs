using ExamPlatform.EntityConfiguration;
using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests
{
    class DatabaseContextTest : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExamPlatformDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;");
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.ApplyConfiguration(new AccountConfiguration());
        //    ////  modelBuilder.ApplyConfiguration(new AccountSecurityConfiguration());
        //    //modelBuilder.ApplyConfiguration(new CourseConfiguration());


        //}
        public DatabaseContextTest(DbContextOptions<DatabaseContextTest> options)
           : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExamPlatformDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
        public DbSet<Accounts> Account { get; set; }
        //public DbSet<AccountSecurity> Security { get; set; }
        


    }
}

    