using System;
using System.Collections.Generic;
using System.Text;

namespace Quill.Models
{
    internal class TextDocument
    {
        private StringBuilder text = new StringBuilder();

        public string Text
        {
            get { return text.ToString(); }
        }
        public void AddCharacter(char c)
        {
            text.Append(c);
        }

        public void RemoveCharacter()
        {
            if (text.Length == 0)
                return;

            text.Length--;
        }
    }
}