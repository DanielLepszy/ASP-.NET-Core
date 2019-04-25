using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.Relations
{
    public class ExamClosedQuestionsConfiguration : IEntityTypeConfiguration<ExamClosedQuestions>
    {
        public void Configure(EntityTypeBuilder<ExamClosedQuestions> builder)
        {

            builder.HasOne(sc => sc.Exam)
                            .WithMany(s => s.ExamClosedQuestions)
                            .HasForeignKey(sc => sc.ExamsID);


            builder.HasOne(sc => sc.ClosedQuestions)
                        .WithMany(s => s.ExamClosedQuestions)
                        .HasForeignKey(sc => sc.ClosedQuestionsId);
        }

    }
}
