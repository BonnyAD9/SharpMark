using System.Collections;

namespace SharpMark.Html;

internal class HtmlElement : IHtmlElement
{
    public string this[string param] => throw new NotImplementedException();

    public HtmlElementType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string Name => throw new NotImplementedException();

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
