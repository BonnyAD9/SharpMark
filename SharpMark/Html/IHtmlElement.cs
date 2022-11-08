namespace SharpMark.Html;

public interface IHtmlElement : IEnumerable<IHtmlElement>
{
    public HtmlElementType Type { get; set; }
    public string Name { get; }
    public string this[string param] { get; }
}
