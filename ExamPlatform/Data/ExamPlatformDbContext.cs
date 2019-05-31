using ExamPlatform.EntityConfiguration;
using ExamPlatformDataModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.Data
{
    public class ExamPlatformDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExamPlatformDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        public DbSet<Accounts> Account { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<ClosedQuestions> ClosedQuestion { get; set; }
        public DbSet<OpenedQuestions> OpenedQuestion { get; set; }
        public DbSet<Exams> Exam { get; set; }
        public DbSet<ExamClosedQuestions> ExamClosedQuestions{ get; set; }
        public DbSet<ExamOpenedQuestions> ExamOpenedQuestions{ get; set; }
        public DbSet<Results> Results{ get; set; }
        public DbSet<EmailAccount> EmailAccount{ get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new ExamClosedQuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new ExamOpenedQuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new ResultsConfiguration());
        }

    }
}
