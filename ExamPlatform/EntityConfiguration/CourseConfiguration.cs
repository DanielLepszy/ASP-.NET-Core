﻿using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasMany(q => q.ClosedQuestionsList)
            .WithOne(c => c.Course)
            .HasForeignKey(c => c.CourseID);

            builder.HasMany(q => q.OpenedQuestionsList)
            .WithOne(c => c.Course)
            .HasForeignKey(c => c.CourseID);

            builder.HasMany(q => q.ExamsList)
           .WithOne(c => c.Course)
           .HasForeignKey(c => c.CourseID);
        }

    }
}

