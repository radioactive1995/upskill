using Microsoft.EntityFrameworkCore;
using Quote_Machine.Models;

namespace Quote_Machine;

public class QuoteContext : DbContext
{
    public QuoteContext(DbContextOptions<QuoteContext> options) : base(options) 
    {
        Database.Migrate();
    }

    public DbSet<Quote> Quotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content);
            entity.Property(e => e.Author);

            entity.HasData(new Quote { Id = 1, Content = "I didn’t fail the test. I just found 100 ways to do it wrong.", Author = "Benjamin Franklin" });
            entity.HasData(new Quote { Id = 2, Content = "Life is 10% what happens to me and 90% of how I react to it.", Author = "Charles Swindoll" });
        });


    }
}