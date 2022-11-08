using SharpMark;

TextReader tr = new StringReader(/* language=markdown */"""
    some text  
    but not full markdown yet
    # Title 1
    new paragraph

    ## Title 2
    """);

Markdown md = new(tr, Console.Out);
md.Process();