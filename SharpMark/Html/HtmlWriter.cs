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

    public void Write(IHtmlElement elem)
    {
        Out.Write($"{_indent}<{elem.Name}");
        foreach (var kvp in elem)
            Out.Write($" {kvp.Key}=\"{kvp.Value}\"");
        Out.WriteLine(">");

        if (elem.Type == HtmlElementType.Double)
            Indent(elem);
    }

    public void Up() => Out.WriteLine($"</{IndentStack.Pop()}>");

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
}
