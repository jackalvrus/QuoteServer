namespace QuoteServer;

public interface IQuoteRepository
{
    Task<string[]> Get(CancellationToken ct);
}
