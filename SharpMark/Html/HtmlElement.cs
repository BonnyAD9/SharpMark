using System.Collections;

namespace SharpMark.Html;

public class HtmlElement : IHtmlElement
{
    private Dictionary<string, string> _params;
    public string this[string param] => _params[param];

    public HtmlTagType Type { get; init; }

    public string Name { get; init; }

    public HtmlElement(string name, HtmlTagType type = HtmlTagType.Single)
    {
        Type = type;
        Name = name;
        _params = new();
    }

    public HtmlElement(string name, Dictionary<string, string> @params, HtmlTagType type = HtmlTagType.Single)
    {
        Type = type;
        Name = name;
        _params = @params;
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _params.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _params.GetEnumerator();
}
