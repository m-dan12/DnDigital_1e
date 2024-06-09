using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DnDigital_1e.Models
{
    public class Node(string title, Node? parent)
    {
        public string Title { get; set; } = title;
        public Node? Parent { get; set; } = parent;
        public bool IsSelected { get; set; }
    }
    public class Folder(string title, Node? parent) : Node(title, parent)
    {
        public ObservableCollection<Node> Content { get; set; } = [];
        public void Add(string title) => Content.Add(new Node(title, this));

        #region Делаем папки приятными для использования
        public Node? this[int index] => Content[index];
        public Node? this[string title] => Content.Single(x => x.Title == title);
        #endregion
    }
    public class HBItemNode(string title, Node? parent, HBItem content) : Node(title, parent)
    {
        public HBItem Content { get; set; } = content;
    }
    public class ChNode(string title, Node? parent, CharacterSheet content) : Node(title, parent)
    {
        public CharacterSheet Content { get; set; } = content;
    }
    public class NoteNode(string title, Node? parent, Note content) : Node(title, parent)
    { 
        public Note Content { get; set; } = content;
    }
    public class HBItem{ }
    public class CharacterSheet{ }
    public class Note{ }
}
