using System.Collections;

namespace SharpMark.Html;

public class HtmlElement : IHtmlElement
{
    private Dictionary<string, string> _params;
    public string this[string param] => _params[param];

    public HtmlTagType Type { get; init; } = HtmlTagType.Single;

    public string Name { get; init; }

    public bool IsClosed { get; init; } = false;

    public IHtmlElement[] Elements { get; init; } = Array.Empty<IHtmlElement>();

    public HtmlElement(string name, HtmlTagType type = HtmlTagType.Single, bool isClosed = false)
    {
        Type = type;
        Name = name;
        IsClosed = isClosed;
        _params = new();
    }

    public HtmlElement(string name, Dictionary<string, string> @params, HtmlTagType type = HtmlTagType.Single, bool isClosed = false)
    {
        Type = type;
        Name = name;
        IsClosed = isClosed;
        _params = @params;
    }

    public HtmlElement(string name, params IHtmlElement[] elems)
    {
        Name = name;
        Elements = elems;
        _params = new();
    }

    public HtmlElement(string name, Dictionary<string, string> @params, params IHtmlElement[] elems)
    {
        Name = name;
        Elements = elems;
        _params = @params;
    }

    public IEnumerable<IHtmlElement> IterateElements()
    {
        foreach (var e in Elements)
            yield return e;
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _params.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _params.GetEnumerator();

    public override string ToString() => Name;
}
