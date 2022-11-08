namespace SharpMark.Html;

public interface IHtmlElement : IEnumerable<KeyValuePair<string, string>>
{
    public HtmlTagType Type { get; }
    public string Name { get; }
    public string this[string param] { get; }
    public bool IsClosed { get; }
    public IEnumerable<IHtmlElement> IterateElements();
}
