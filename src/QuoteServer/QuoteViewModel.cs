namespace QuoteServer;

public class QuoteViewModel
{
    public QuoteViewModel(string quote)
    {
        var parts = quote.Split('\t', 2);
        Author = parts[0];
        Text = parts[1];
    }

    public string Author { get; }
    public string Text { get; }
}