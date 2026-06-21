using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Logging;

namespace Quill.Models;
public class TextDocument
{
    public string FileName { get; set; } = "Untitled";
    public string Content { get; set; } = "";
    public string Extension { get; set; } = ".wrt";
    
    public string? Directory { get; set; }

    public bool IsModified { get; set; }
    
    
    
}
