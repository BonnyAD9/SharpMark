using SharpMark;

TextReader tr = new StringReader("some text\nbut not full markdown yet");
Markdown md = new(tr, Console.Out);
md.Process();