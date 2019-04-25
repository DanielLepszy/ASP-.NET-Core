using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.EntityConfiguration
{
    public class ExamOpenedQuestionsConfiguration : IEntityTypeConfiguration<ExamOpenedQuestions>
    {
        public void Configure(EntityTypeBuilder<ExamOpenedQuestions> builder)
        {

            builder.HasOne(sc => sc.Exam)
                     .WithMany(s => s.ExamOpenedQuestions)
                     .HasForeignKey(sc => sc.ExamsID);


            builder.HasOne(sc => sc.OpenedQuestions)
                        .WithMany(s => s.ExamOpenedQuestions)
                        .HasForeignKey(sc => sc.OpenedQuestionsID);
        }
    }
}
