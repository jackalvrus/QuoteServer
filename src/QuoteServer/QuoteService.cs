namespace QuoteServer;

public class QuoteService
{
    private readonly IQuoteRepository repository;

    private readonly Random random = new();
    private readonly SemaphoreSlim cacheLock = new(1, 1);
    private string[]? quotesCache = null;

    public QuoteService(IQuoteRepository repository)
    {
        this.repository = repository;
    }

    public async Task<string> GetRandom(CancellationToken ct)
    {
        var quotes = await Ensure(ct);
        return quotes[random.Next(quotes.Length)];
    }

    public async Task Refresh(CancellationToken ct)
    {
        var newQuotes = await Load(ct);
        await cacheLock.WaitAsync(ct);
        try
        {
            quotesCache = newQuotes;
        }
        finally
        {
            cacheLock.Release();
        }
    }

    private async Task<string[]> Ensure(CancellationToken ct)
    {
        if (quotesCache is null)
        {
            await cacheLock.WaitAsync(ct);
            try
            {
                if (quotesCache is null)
                {
                    quotesCache = await Load(ct);
                }
            }
            finally
            {
                cacheLock.Release();
            }
        }
        return quotesCache;
    }

    private Task<string[]> Load(CancellationToken ct) => repository.Get(ct);
}
