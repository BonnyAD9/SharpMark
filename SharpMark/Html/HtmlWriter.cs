using System.Text;

namespace SharpMark.Html;

public class HtmlWriter
{
    public TextWriter Out { get; init; }
    public Stack<string> IndentStack { get; init; } = new();
    public int Indentation
    {
        get => IndentStack.Count;
    }
    private string _indent = "";
    public string IndentStr { get; init; } = "    ";

    public HtmlWriter(TextWriter @out)
    {
        Out = @out;
    }

    public void Write(IHtmlElement elem, bool close = false)
    {
        if (elem.Type == HtmlTagType.Plain)
        {
            var str = elem["value"].Replace("\n", '\n' + _indent);
            if (str.Length > 0)
                Out.WriteLine(_indent + str);
            return;
        }

        Out.Write($"{_indent}<{elem.Name}");
        foreach (var kvp in elem)
            Out.Write($" {kvp.Key}=\"{kvp.Value}\"");
        Out.WriteLine(">");

        if (elem.Type != HtmlTagType.Double)
            return;
        
        Indent(elem);
        foreach (var e in elem.IterateElements())
            Write(e, true);

        if (elem.IsClosed || close)
            Up();
    }

    public void Up() => Out.WriteLine($"{_indent = _indent[..^IndentStr.Length]}</{IndentStack.Pop()}>");

    private void Indent(IHtmlElement elem)
    {
        IndentStack.Push(elem.Name);
        StringBuilder sb = new(IndentStr.Length * Indentation);
        for (int i = 0; i < Indentation; i++)
            sb.Append(IndentStr);
        _indent = sb.ToString();
    }

    public void End()
    {
        while (Indentation != 0)
            Up();
    }

    public void Setup()
    {
        Write(new PlainElement("<!DOCTYPE html>"));
        Write(new HtmlElement("html", HtmlTagType.Double));
        Write(new HtmlElement("head", HtmlTagType.Double));
        Up();
        Write(new HtmlElement("body", HtmlTagType.Double));
    }
}
