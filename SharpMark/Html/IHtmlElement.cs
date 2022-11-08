namespace SharpMark.Html;

public interface IHtmlElement : IEnumerable<KeyValuePair<string, string>>
{
    public HtmlElementType Type { get; set; }
    public string Name { get; }
    public string this[string param] { get; }
}
