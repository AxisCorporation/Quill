using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Quill.Models;

namespace Quill.ViewModels;

public static class RecentDocumentsViewModel
{
    public static ObservableCollection<TextDocument> Documents { get; } = new();

    public static void Add(TextDocument doc)
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
    }
    
}