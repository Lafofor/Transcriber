using Microsoft.EntityFrameworkCore;
using Transcriber.Models;

public class TranscriptionContext : DbContext
{
    public TranscriptionContext(DbContextOptions<TranscriptionContext> options) : base(options) { }

    public DbSet<TranscriptionRequest> TranscriptionRequests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TranscriptionRequest>()
            .HasOne(t => t.User)
            .WithMany(u => u.TranscriptionRequests)
            .HasForeignKey(t => t.UserId);
    }
}
