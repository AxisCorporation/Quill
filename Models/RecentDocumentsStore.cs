using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Quill.Models
{
    public static class RecentDocumentsStore
    { //In memory central storage for recently accessed docs
      //Also shared to HomeViewModel and EditorViewModel
      //Used Observation COllection so UI updates by itself when item changes
        public static ObservableCollection<RecentDocument> Documents { get; } = new();

        public static void Add(string filePath)
        {
            //ignore invald route
            if (string.IsNullOrWhiteSpace(filePath))
                return;
            //if document already exists in list, removes it to reinsert it at top(recent)
            var existing = Documents.FirstOrDefault(x => x.FilePath == filePath);

            if (existing != null)
                Documents.Remove(existing);

            //puts new doc at top(first/recent)
            Documents.Insert(0, new RecentDocument
            {
                FileName = Path.GetFileName(filePath),
                FilePath = filePath
            });

            //limit list size
            while (Documents.Count > 10)
                Documents.RemoveAt(Documents.Count - 1);
        }
    }
}
