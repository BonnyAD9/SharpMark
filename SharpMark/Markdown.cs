using SharpMark.Html;
using System.Text;

namespace SharpMark;

public class Markdown
{
    public TextReader In { get; init; }
    public HtmlWriter Out { get; init; }
    char _cur = ' ';
    bool IsEof => _cur == char.MaxValue;

    public Markdown(TextReader @in, TextWriter @out)
    {
        In = @in;
        Out = new(@out);
        Out.Setup();
    }

    public void Process()
    {
        Next();
        foreach (var e in ReadElements())
            Out.Write(e);
        Out.End();
    }

    private IEnumerable<IHtmlElement> ReadElements()
    {
        StringBuilder sb = new();
        do
        {
            var prev = _cur;
            if (Next() == char.MaxValue)
            {
                sb.Append(prev);
                break;
            }

            if (_cur == '\r')
            {
                _cur = '\n';
                if (Peek() == '\n')
                    Next();
            }

            if (prev == '\n' && _cur == '\n')
            {
                yield return DumpSB();
                sb.Clear();
                continue;
            }

            if (prev == '\n' && _cur == '#')
            {
                yield return DumpSB();
                sb.Clear();
                int c = 1;
                while (Next() == '#')
                    c++;
                ReadLine(sb);
                yield return new HtmlElement($"h{c}", new PlainElement(sb.ToString())) { Type = HtmlTagType.Double, IsClosed = true };
                sb.Clear();
            }

            if (_cur == '\n' && prev == ' ' && sb.Length > 0 && sb[^1] == ' ')
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append("\n<br>");
            }

            sb.Append(prev);
        }
        while (!IsEof);

        if (sb[^1] == '\n')
            sb.Remove(sb.Length - 1, 1);

        if (sb.Length == 0)
            yield break;

        yield return DumpSB();

        IHtmlElement DumpSB()
        {
            var str = sb.ToString().Trim();
            sb.Clear();
            if (str.Length == 0)
                return new PlainElement("");
            return new HtmlElement("p", new PlainElement(str)) { Type = HtmlTagType.Double, IsClosed = true };
        }
    }

    private IHtmlElement OldReadElement()
    {
        StringBuilder sb = new();
        do sb.Append(_cur);
        while ((_cur != '\n' | Next() != '\n') && !IsEof);
        if (sb[^1] == '\n')
            sb.Remove(sb.Length - 1, 1);

        return new HtmlElement("p", new PlainElement(sb.ToString())) { Type = HtmlTagType.Double, IsClosed = true };
    }

    private char Next()
    {
        var c = In.Read();
        return _cur = c == -1 ? char.MaxValue : (char)c;
    }

    private char Peek()
    {
        var c = In.Peek();
        return c == -1 ? char.MinValue : (char)c;
    }

    private void ReadLine(StringBuilder @out)
    {
        do @out.Append(_cur);
        while (Next() != '\n' && _cur != '\r' && !IsEof);
    }
}
