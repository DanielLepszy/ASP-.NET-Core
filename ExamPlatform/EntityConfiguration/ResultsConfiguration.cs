using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.EntityConfiguration
{
    public class ResultsConfiguration : IEntityTypeConfiguration<Results>
    {
        public void Configure(EntityTypeBuilder<Results> builder)
        {
            builder.HasOne(s => s.Exam)
                    .WithOne(e => e.ExamResult)
                    .HasForeignKey<Results>(s => s.ExamID);
        }
    }
}
