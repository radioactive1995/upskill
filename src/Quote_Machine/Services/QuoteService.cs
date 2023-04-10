using Microsoft.EntityFrameworkCore;
using Quote_Machine.Models;

namespace Quote_Machine.Services;

public interface IQuoteService
{
    Task<(bool, Quote[])> TryGetQuoteListAsync(CancellationToken cancellationToken = default);
    Task<bool> TryUpdateQuoteAsync(Quote quote, CancellationToken cancellationToken = default);
    Task<bool> TryAddQuoteAsync(QuoteDto quoteDto, CancellationToken cancellationToken = default);
    Task<bool> TryDeleteQuoteAsync(Quote quote, CancellationToken cancellationToken = default);
}

public class QuoteService : IQuoteService
{
    private readonly IDbContextFactory<QuoteContext> _dbFactory;
    public QuoteService(IDbContextFactory<QuoteContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> TryAddQuoteAsync(QuoteDto quoteDto, CancellationToken cancellationToken = default)
    {
        var quote = new Quote
        {
            Content = quoteDto.Content,
            Author = quoteDto.Author
        };

        try
        {
            await using var db = await _dbFactory.CreateDbContextAsync(cancellationToken);
            await db.Quotes.AddAsync(quote, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> TryDeleteQuoteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var db = await _dbFactory.CreateDbContextAsync(cancellationToken);
            db.Entry(quote).State = EntityState.Deleted;
            await db.SaveChangesAsync(cancellationToken);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> TryUpdateQuoteAsync(Quote quote, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var db = await _dbFactory.CreateDbContextAsync(cancellationToken);
            db.Entry(quote).State = EntityState.Modified;
            await db.SaveChangesAsync(cancellationToken);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<(bool, Quote[])> TryGetQuoteListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await using var db = await _dbFactory.CreateDbContextAsync(cancellationToken);
            return (true, await db.Quotes.ToArrayAsync());
        }

        catch (Exception ex)
        {
            return (false, Array.Empty<Quote>());
        }
    }
}