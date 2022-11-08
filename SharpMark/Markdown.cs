using SharpMark.Html;

namespace SharpMark;

public class Markdown
{
    public TextReader In { get; init; }
    public HtmlWriter Out { get; init; }

    public Markdown(TextReader @in, TextWriter @out)
    {
        In = @in;
        Out = new(@out);
        Out.Setup();
    }

    public void Process()
    {
        string? str;
        while ((str = In.ReadLine()) is not null)
        {
            Out.Write(new PlainElement(str));
        }
        Out.End();
    }
}
