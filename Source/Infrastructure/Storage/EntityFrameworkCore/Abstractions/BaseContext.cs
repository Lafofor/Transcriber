using Core.SpeechToText.Enums;
using Microsoft.EntityFrameworkCore;
using Storage.EntityFrameworkCore.Models;

namespace Storage.EntityFrameworkCore.Abstractions;

public abstract class BaseContext : DbContext
{
    public DbSet<Operation> Operations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Operation>
        (
            entity =>
            {
                entity.Property(o => o.Status).HasConversion<string>
                (
                    status => status.ToString(),
                    value => Enum.Parse<OperationStatus>(value)
                );
            }
        );
    }
}