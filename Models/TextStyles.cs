using Avalonia.Media;
using AvRichTextBox;

namespace Quill.Models;

public class DocumentStyle
{
    public string Name { get; set; } = "";

    public FontFamily FontFamily { get; set; }
        = new("Meiryo");

    public double FontSize { get; set; } = 16;

    public FontWeight FontWeight { get; set; }
        = FontWeight.Normal;

    public FontStyle FontStyle { get; set; }
        = FontStyle.Normal;

    public TextAlignment Alignment { get; set; }
        = TextAlignment.Left;
}

