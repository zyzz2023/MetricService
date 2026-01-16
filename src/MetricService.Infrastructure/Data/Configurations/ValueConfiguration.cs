using MetricService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Configurations;

internal class ValueConfiguration : IEntityTypeConfiguration<MetricValue>
{
    public void Configure(EntityTypeBuilder<MetricValue> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Date)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(v => v.ExecutionTime)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(v => v.Value)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.HasOne(v => v.Result)
            .WithMany(r => r.MetricValues)
            .HasForeignKey(v => v.ResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
