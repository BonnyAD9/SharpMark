using System.Collections;

namespace SharpMark.Html;

public class PlainElement : IHtmlElement
{
    private string _value;
    public string this[string param] => _value;

    public HtmlTagType Type => HtmlTagType.Plain;

    public string Name => _value;

    public PlainElement(string value)
    {
        _value = value;
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        yield return new("value", _value);
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
