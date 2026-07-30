using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Quill.Models;

namespace Quill.ViewModels;

public static class RecentDocumentsViewModel
{
    public static ObservableCollection<TextDocument> Documents { get; } = new();
    
    private static readonly string FolderPath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "YourAppName");
    private static readonly string RecentDocumentsFile =
        Path.Combine(FolderPath, "recentdocuments.json");

    public static async Task Add(TextDocument doc)
    {
        //In memory central storage for recently accessed docs
        //Also shared to HomeViewModel and EditorViewModel
        //Used Observation Collection so UI updates by itself when item changes
        
        //ignore invalid route
        if (string.IsNullOrWhiteSpace(doc.FilePath))
            return;
        
        //if document already exists in list, removes it to reinsert it at top(recent)
        var existing = Documents.FirstOrDefault(x => x.FilePath == doc.FilePath);

        if (existing != null)
            Documents.Remove(existing);

        //puts new doc at top(first/recent)
        Documents.Insert(0, new TextDocument()
        {
            FileName = Path.GetFileName(doc.FileName),
            Directory = doc.Directory,
        });

        //limit list size
        while (Documents.Count > 10)
            Documents.RemoveAt(Documents.Count - 1);
        
        await SaveAsync();
    }
    

    public static async Task SaveAsync()
    {
        Directory.CreateDirectory(FolderPath);

        await using var stream = File.Create(RecentDocumentsFile);

        await JsonSerializer.SerializeAsync(
            stream,
            Documents,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
    }
    
    // Make this Async later
    public static void Load()
    {
        if (!File.Exists(RecentDocumentsFile))
            return;

        try
        {
            var json = File.ReadAllText(RecentDocumentsFile);

            var docs = JsonSerializer.Deserialize<List<TextDocument>>(json);

            Documents.Clear();

            if (docs is null)
                return;

            foreach (var doc in docs)
                Documents.Add(doc);
        }
        catch (Exception ex)
        {
            // TODO: Log the exception if you have a logging system.
            // For now, you could simply ignore it or notify the user.

            Console.WriteLine(ex);
        }
    }
    
}