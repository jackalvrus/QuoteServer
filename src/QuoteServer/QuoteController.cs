using Microsoft.AspNetCore.Mvc;

namespace QuoteServer;

[ApiController]
[Route("[controller]")]
public class QuoteController : ControllerBase
{
    private readonly QuoteService quoteService;
    private readonly ILogger<QuoteController> logger;

    public QuoteController(
        QuoteService quoteService,
        ILogger<QuoteController> logger)
    {
        this.quoteService = quoteService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<QuoteViewModel> Get(CancellationToken ct) => new(await quoteService.GetRandom(ct));
}