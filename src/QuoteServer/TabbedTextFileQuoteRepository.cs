using Microsoft.Extensions.Options;

namespace QuoteServer;

public class TabbedTextFileQuoteRepository : IQuoteRepository
{
    private readonly IOptionsMonitor<TabbedTextFileOptions> options;
    private readonly ILogger<TabbedTextFileQuoteRepository> logger;

    public TabbedTextFileQuoteRepository(
        IOptionsMonitor<TabbedTextFileOptions> options,
        ILogger<TabbedTextFileQuoteRepository> logger)
    {
        this.options = options;
        this.logger = logger;
    }

    public async Task<string[]> Get(CancellationToken ct)
    {
        string filePath = options.CurrentValue.Path;
        logger.LogInformation("File Path from Options {FilePath}", filePath);
        var data = await File.ReadAllLinesAsync(filePath, ct);
        return data.Where(s => !string.IsNullOrWhiteSpace(s) && s.Contains('\t')).ToArray();
    }
}
