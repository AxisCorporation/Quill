using System.Text;

namespace Quill.Models;
internal class TextDocument
{
    private StringBuilder text = new();

    public string Text
    {
        get => text.ToString(); 
    }
    public void AddCharacter(char c)
    {
        text.Append(c);
    }

    public void RemoveCharacter()
    {
        if (text.Length == 0)
            return;

        text.Remove(text.Length - 1, 1);
    }
}
