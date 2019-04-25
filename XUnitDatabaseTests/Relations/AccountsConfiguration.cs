using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.Relations
{
    class AccountsConfiguration : IEntityTypeConfiguration<Accounts>
    {
       
            public void Configure(EntityTypeBuilder<Accounts> builder)
            {
            builder.HasMany(e => e.SelectedExam)
            .WithOne(a => a.Account)
            .HasForeignKey(a => a.AccountID);
        }
     }
}
