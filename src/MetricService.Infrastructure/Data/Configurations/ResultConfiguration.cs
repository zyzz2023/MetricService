using MetricService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Configurations;

public class ResultConfiguration : IEntityTypeConfiguration<FileResult>
{
    public void Configure(EntityTypeBuilder<FileResult> builder)
    {
        builder.ToTable("Results");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.FileName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.DeltaTimeSeconds)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(r => r.StartDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(r => r.EndDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(r => r.AverageExecutionTime)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(r => r.AverageValue)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(r => r.MedianValue)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(r => r.MinValue)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(r => r.MaxValue)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.HasIndex(r => r.FileName)
            .IsUnique()
            .HasDatabaseName("IX_Results_FileName");
    }
}
