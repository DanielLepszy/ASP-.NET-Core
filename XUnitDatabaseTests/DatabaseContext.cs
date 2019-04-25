using ExamPlatform.EntityConfiguration;
using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using System;
using XUnitDatabaseTests.Relations;
using ExamClosedQuestionsConfiguration = XUnitDatabaseTests.Relations.ExamClosedQuestionsConfiguration;
using ExamOpenedQuestionsConfiguration = XUnitDatabaseTests.Relations.ExamOpenedQuestionsConfiguration;
using ResultsConfiguration = XUnitDatabaseTests.Relations.ResultsConfiguration;

namespace XUnitDatabaseTests
{
    public class DatabaseContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("DataSource=:memory:", x => { });
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfiguration());
            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.ApplyConfiguration(new ExamOpenedQuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new ExamClosedQuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new ResultsConfiguration());
          
   
    }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
               : base()
        { }
        public DbSet<Accounts> Account { get; set; }
        public DbSet<Exams> Exams { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<ClosedQuestions> ClosedQuestions { get; set; }
        public DbSet<OpenedQuestions> OpenedQuestions { get; set; }
        public DbSet<ExamClosedQuestions> ExamClosedQuestions { get; set; }
        public DbSet<ExamOpenedQuestions> ExamOpenedQuestions { get; set; }
        public DbSet<Results> Results { get; set; }
        
    }
}

