using QuoteServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<QuoteService>();
builder.Services.AddSingleton<IQuoteRepository, TabbedTextFileQuoteRepository>();
builder.Services.Configure<TabbedTextFileOptions>(builder.Configuration.GetSection(TabbedTextFileOptions.SectionName));

//TODO builder.Services.AddSingleton<Miner>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        policy => policy
            .WithOrigins(
                "http://roadraceautox.com",
                "http://www.roadraceautox.com",
                "https://roadraceautox.com",
                "https://www.roadraceautox.com")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.

//TODO: do we need HTTPS redir?
//app.UseHttpsRedirection();

app.MapControllers();
app.UseCors();
app.Run();
