using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DnDigital_1e.Models
{
    #region Один класс с object Content, несколько типов контентов
    /*
    public class Node
    {
        public string Title { get; set; }
        public Node? Parent { get; set; }
        public bool IsSelected { get; set; }
        public object Content { get; set; }
        public Node(string title, Node? parent, HandbookItem content) // Узел справочника
        {
            Title = title;
            Parent = parent;
            Content = content;
        }
        public Node(string title, Node? parent, CharacterSheet content) // Узел чарника
        {
            Title = title;
            Parent = parent;
            Content = content;
        }
        public Node(string title, Node? parent, AdventureNote content) // Узел приключения
        {
            Title = title;
            Parent = parent;
            Content = content;
        }
        public Node(string title, Node? parent) // Узел папки
        {
            Title = title;
            Parent = parent;
            Content = new NodeCollection(this);
        }
    }
    public class NodeCollection(Node parent)
    {
        private readonly Node parent = parent;
        ObservableCollection<Node> Nodes { get; set; } = [];
        public void Add(string title, HandbookItem node) => Nodes.Add(new Node(title, parent, node));
        public void Add(string title, CharacterSheet node) => Nodes.Add(new Node(title, parent, node));
        public void Add(string title, AdventureNote node) => Nodes.Add(new Node(title, parent, node));
        public void Add(string title) => Nodes.Add(new Node(title, parent));
        public Node this[int index] => Nodes[index];
        public Node this[string title] => Nodes.Single(x => x.Title == title);

    }
    public class HandbookItem{ }
    public class CharacterSheet{ }
    public class AdventureNote{ }
    */
    #endregion

    #region Несколько классов узлов, наследуемых от Node. Каждый со своим типом контента
    /*public abstract class Node
    {
        public string Title { get; set; }
        public Node? Parent { get; set; } = null;
        public bool IsSelected { get; set; }

        public Node(string title) => Title = title;
        public Node(string title, Node? parent)
        {
            Title = title;
            Parent = parent;
        }
    }
    public class HandbookNode : Node
    {
        public HandbookItem Content { get; set; }

        public HandbookNode(string title, HandbookItem content) : base(title) => Content = content;
        public HandbookNode(string title, Node parent, HandbookItem content) : base(title, parent) => Content = content;
    }
    public class CharacterNode : Node
    {
        public CharacterSheet Content { get; set; }

        public CharacterNode(string title, CharacterSheet content) : base(title) => Content = content;
        public CharacterNode(string title, Node parent, CharacterSheet content) : base(title, parent) => Content = content;
    }
    public class AdventureNode : Node
    {
        public AdventureNote Content { get; set; }

        public AdventureNode(string title, AdventureNote content) : base(title) => Content = content;
        public AdventureNode(string title, Node parent, AdventureNote content) : base(title, parent) => Content = content;
    }
    public class FolderNode : Node
    {
        ObservableCollection<Node> Content { get; set; } = [];

        public FolderNode(string title) : base(title) { }
        public FolderNode(string title, Node parent) : base(title, parent) { }

        public void Add(string title, HandbookItem node) => Content.Add(new HandbookNode(title, this, node));
        public void Add(string title, CharacterSheet node) => Content.Add(new CharacterNode(title, this, node));
        public void Add(string title, AdventureNote node) => Content.Add(new AdventureNode(title, this, node));
        public void Add(string title) => Content.Add(new FolderNode(title, this));
        public Node this[int index] => Content[index];
        public Node this[string title] => Content.Single(x => x.Title == title);

    }
    public class HandbookItem{ }
    public class CharacterSheet{ }
    public class AdventureNote{ }*/
    #endregion

    #region Один обобщенный класс с несколькими типами контента
    /*public interface INodeContent { }
    public class HandbookItem : INodeContent { }
    public class CharacterSheet : INodeContent { }
    public class AdventureNote : INodeContent { }
    public class NodeFolder : ObservableCollection<Node<INodeContent>>, INodeContent
    {
        public void Add(string title, Node<INodeContent> parent, INodeContent content)
        {
            Add(new Node<INodeContent>(title, parent, content));
        }
    }
    public class Node<T> where T : INodeContent
    {
        public string Title { get; set; }
        public Node<T>? Parent { get; set; } = null;
        public bool IsSelected { get; set; }
        public T Content { get; set; }
        public Node(string title, T content)
        {
            Title = title;
            Content = content;
        }
        public Node(string title, Node<T> parent, T content)
        {
            Title = title;
            Parent = parent;
            Content = content;
        }
    }*/
    #endregion

    #region Один обобщенный класс с несколькими типами контента
    /*public interface INodeContent { }
    public class HandbookItem : INodeContent { }
    public class CharacterSheet : INodeContent { }
    public class AdventureNote : INodeContent { }
    public class NodeFolder<T> : ObservableCollection<T>, INodeContent where T : Node<INodeContent>
    {
        public T this[int index] => this[index];
    }
    public class Node<T>(T content) where T : INodeContent
    {
        public T Content { get; set; } = content;
    }
    public static class TExtension
    {
        public static void Add(this Node<NodeFolder<Node<INodeContent>>> collection, Node<INodeContent> node) => collection.Content.Add(node);
    }*/
    #endregion

    #region Один обобщенный класс с несколькими типами контента
    /*public interface INodeContent { }
    public class HandbookItem : INodeContent { }
    public class CharacterSheet : INodeContent { }
    public class AdventureNote : INodeContent { }
    public class Node
    {
        public dynamic Content { get; set; }
        public Node() => Content = new ObservableCollection<Node>();
        public Node(INodeContent content) => Content = content;
    }*/
    #endregion

    #region Один обобщенный класс с несколькими типами контента
    /*public interface IContent { }
    public class HandbookItem : IContent { }
    public class CharacterSheet : IContent { }
    public class AdventureNote : IContent { }

    public interface INode
    {
        public string Title { get; set; }
    }
    public class TreeTop(string title) : INode
    {
        public string Title { get; set; } = title;
        public ObservableCollection<INode> Content { get; set; } = [];
        public void Add(string title, IContent content) => Content.Add(new Node(title, this, content));
        public void Add(string title) => Content.Add(new Folder(title, this));
        public INode this[int index] => Content[index];
        public INode this[string title] => Content.Single(x => x.Title == title);
    }
    public class Node(string title, INode parent, IContent content) : INode
    {
        public string Title { get; set; } = title;
        public INode Parent { get; set; } = parent;
        public bool IsSelected { get; set; } = false;
        public IContent Content { get; set; } = content;
    }
    public class Folder(string title, INode parent) : INode
    {
        public string Title { get; set; } = title;
        public INode Parent { get; set; } = parent;
        public bool IsSelected { get; set; } = false;
        public ObservableCollection<INode> Content { get; set; } = [];
        public void Add(string title, IContent content) => Content.Add(new Node(title, this, content));
        public void Add(string title) => Content.Add(new Folder(title, this));
        public INode this[int index] => Content[index];
        public INode this[string title] => Content.Single(x => x.Title == title);
    }*/
    #endregion

    #region Почти гуд
    /*public class Nodes<T> where T : IContent
    {
        public string Title { get; set; }
        public Nodes<T>? Parent { get; set; }
        public ObservableCollection<Nodes<T>>? Subnodes { get; set; }
        public T? Content { get; set; }
        public Nodes(IContent content)
        {
            Content = (T)content;
            Title = Content.Title;
        }
        public Nodes(string title)
        {
            Title = title;
            Subnodes = [];
        }
        public void Add(string title)
        {
            if (Subnodes != null)
                Subnodes.Add(new(title));
        }
        public void Add(IContent content)
        {
            if (Subnodes != null)
                Subnodes.Add(new(content));
        }
        public Nodes<T> this[int index] => Subnodes[index];
        public Nodes<T> this[string title] => Subnodes.Single(x => x.Title == title);
    }

    public interface IContent
    {
        public string Title { get; set; }
    }
    public class HandbookItem(string title) : IContent
    {
        public string Title { get; set; } = title;

    }
    public class CharacterSheet(string name) : IContent
    {
        public string Title { get => Name; set => Name = Title; }
        public string Name { get; set; } = name;
    }
    public class AdventureNote(string title) : IContent
    {
        public string Title { get; set; } = title;
    }
    public interface INode
    {
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
    public interface IFolder
    {
        public ObservableCollection<INode> Content { get; set; }
    }
    public class TreeTop(string title) : IFolder
    {
        public string Title { get; set; } = title;
        public ObservableCollection<INode> Content { get; set; } = [];

        public void Add(IContent content) => Content.Add(new Node(content.Title, this, content));
        public void Add(string title) => Content.Add(new Folder(title, this));

        public INode this[int index] => Content[index];
        public INode this[string title] => Content.Single(x => x.Title == title);
    }
    public class Node(string title, IFolder parent, IContent content) : INode
    {
        public string Title { get; set; } = title;
        public IFolder Parent { get; set; } = parent;
        public bool IsSelected { get; set; } = false;
        public IContent Content { get; set; } = content;
    }
    public class Folder(string title, IFolder parent) : IFolder, INode
    {
        public string Title { get; set; } = title;
        public IFolder Parent { get; set; } = parent;
        public bool IsSelected { get; set; } = false;
        public ObservableCollection<INode> Content { get; set; } = [];

        public void Add(IContent content) => Content.Add(new Node(content.Title, this, content));
        public void Add(string title) => Content.Add(new Folder(title, this));

        public INode this[int index] => Content[index];
        public INode this[string title] => Content.Single(x => x.Title == title);
    }*/
    #endregion

    #region Благослови меня Господь, на гуд структуру Node-ов
    public abstract class Node
    {
        public string Title { get; set; }
        public abstract object Content { get; }
    }
    public class FolderNode : Node
    {
        private NodeCollection nodes { get; set; } = [];
        public override object Content => nodes;
        public FolderNode(string title)
        {
            Title = title;
        }

        #region Методы Get и Add
        public void Add(string title) => nodes.Add(new FolderNode(title));
        public void Add(HandbookItem content) => nodes.Add(new HandbookNode(content));
        public void Add(CharacterSheet content) => nodes.Add(new CharacterNode(content));
        public void Add(AdventureNote content) => nodes.Add(new AdventureNode(content));

        public void AddRange(List<string> titles) => titles.ForEach(x => nodes.Add(new FolderNode(x)));
        public void AddRange(List<HandbookItem> contents) => contents.ForEach(x => nodes.Add(new HandbookNode(x)));
        public void AddRange(List<CharacterSheet> contents) => contents.ForEach(x => nodes.Add(new CharacterNode(x)));
        public void AddRange(List<AdventureNote> contents) => contents.ForEach(x => nodes.Add(new AdventureNode(x)));

        public Node this[int index] => nodes[index];
        public Node this[string title] => nodes.Single(x => x.Title == title);
        #endregion
    }
    public class HandbookNode : Node
    {
        private HandbookItem _content { get; set; }
        public override object Content => _content;
        public HandbookNode(HandbookItem content)
        {
            Title = content.Title;
            _content = content;
        }
    }
    public class CharacterNode : Node
    {
        private CharacterSheet _content { get; set; }
        public override object Content => _content;
        public CharacterNode(CharacterSheet content)
        {
            Title = content.Name;
            _content = content;
        }
    }
    public class AdventureNode : Node
    {
        private AdventureNote _content { get; set; }
        public override object Content => _content;
        public AdventureNode(AdventureNote content)
        {
            Title = content.Title;
            _content = content;
        }
    }
    public class HandbookItem(string title)
    {
        public string Title { get; set; } = title;
    }
    public class CharacterSheet(string name)
    {
        public string Name { get; set; } = name;
    }
    public class AdventureNote(string title)
    {
        public string Title { get; set; } = title;
    }
    public class NodeCollection : ObservableCollection<Node> { }

    #endregion

    #region Благослови меня Господь, на гуд структуру Node-ов

    /*public class HandbookFolder(string title)
    {
        public string Title { get; set; } = title;
        public ObservableCollection<HandbookNode> Nodes { get; set; } = [];
        public HandbookNode this[string title] => Nodes.Single(x => x.Title == title);
        public void Add(string title) => Nodes.Add(new(title));
    }
    public class HandbookNode(string title)
    {
        public string Title => Content.Title;
        public HandbookItem Content { get; set; } = new(title);
    }
    public class HandbookItem(string title)
    {
        public string Title { get; set; } = title;
    }

    public class CharactersFolder(string title) : ObservableCollection<CharacterNode>
    {
        public string Title { get; set; } = title;
        public CharacterNode this[string title] => this.Single(x => x.Title == title);
    }
    public class CharacterNode(CharacterSheet content)
    {
        public string Title => Content.Name;
        public CharacterSheet Content { get; set; } = content;
    }
    public class CharacterSheet(string name)
    {
        public string Name { get; set; } = name;
    }

    public class AdventuresFolder(string title) : ObservableCollection<AdventureNode>
    {
        public string Title { get; set; } = title;
        public AdventureNode this[string title] => this.Single(x => x.Title == title);
    }
    public class AdventureNode(AdventureNote content)
    {
        public string Title => Content.Title;
        public AdventureNote Content { get; set; } = content;
    }
    public class AdventureNote(string title)
    {
        public string Title { get; set; } = title;
    }*/


    #endregion


}
