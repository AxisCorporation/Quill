using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Quill.Models;
internal static class TextDocument
{
    public static event Action<string>? FilePathChanged; 

    public static string? CurrentFilePath 
    { 
        get; 
        set
        {
            field = value;
            FilePathChanged?.Invoke(value!);
        } 
    } 

    /// <summary>
    /// An async extension method for overwriting the current open file with the specified contents.
    /// </summary>
    /// <param name="Contents">Content to overwrite the family with.</param>
    /// <returns>False if `CurrentFilePath` is null</returns>
    public static async Task<bool> WriteToFile(this string Contents)
    {
        if (CurrentFilePath is null)
        {
            return false;
        }

        await File.WriteAllTextAsync(CurrentFilePath, Contents);
        return true;
    }

    public static async Task<bool> AppendToFile(this string Contents)
    {
        if (CurrentFilePath is null)
        {
            return false;
        }

        await File.AppendAllTextAsync(CurrentFilePath, Contents);
        return true;
    }
}
