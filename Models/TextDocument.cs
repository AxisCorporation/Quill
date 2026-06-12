using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Logging;

namespace Quill.Models;
internal static class TextDocument
{
    public static string? CurrentFilePath 
    { 
        get 
        {
            if (string.IsNullOrEmpty(CurrentDirectory) || string.IsNullOrEmpty(CurrentFileName) || string.IsNullOrEmpty(CurrentFileExtension))
            {
                return null;
            }

            return $"{CurrentDirectory}/{CurrentFileName}.{CurrentFileExtension}";
        }
    } 

    public static string? CurrentFileName { get; set; }
    public static string? CurrentFileExtension { get; set; }
    public static string? CurrentDirectory { get; set; }

    /// <summary>
    /// An async method for overwriting the current open file with the specified contents.
    /// </summary>
    /// <param name="Contents">Content to overwrite the family with.</param>
    /// <returns>False if `CurrentFilePath` is null</returns>
    public static async Task<bool> WriteToFileAsync(string? Contents)
    {
        if (CurrentFilePath is null)
        {
            return false;
        }

        await File.WriteAllTextAsync(CurrentFilePath, Contents);
        return true;
    }

    /// <summary>
    /// An async method for appending the specified contents to the current open file.
    /// </summary>
    /// <param name="Contents">Content to overwrite the family with.</param>
    /// <returns>False if `CurrentFilePath` is null</returns>
    public static async Task<bool> AppendToFileAsync(string Contents)
    {
        if (CurrentFilePath is null)
        {
            return false;
        }

        await File.AppendAllTextAsync(CurrentFilePath, Contents);
        return true;
    }
}
