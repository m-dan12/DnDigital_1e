using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Collections.Generic;
using System.Linq;
using System;
using DnDigital_1e.Models;
using System.Collections.ObjectModel;


namespace DnDigital_1e.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    #region Системные кнопки
    // Объявление команд
    private ReactiveCommand<Unit, Unit>? _closeCommand;
    private ReactiveCommand<Unit, Unit>? _hideCommand;
    private ReactiveCommand<Unit, Unit>? _expandCommand;

    // Инициализация команд
    public ReactiveCommand<Unit, Unit> CloseCommand => _closeCommand ??= ReactiveCommand.Create(() => {
        var window = (App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        window.Close();
    });
    public ReactiveCommand<Unit, Unit> HideCommand => _hideCommand ??= ReactiveCommand.Create(() => {
        var window = (App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        window.WindowState = WindowState.Minimized;
    });
    public ReactiveCommand<Unit, Unit> ExpandCommand => _expandCommand ??= ReactiveCommand.Create(() => {
        var window = (App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    });

    #endregion

    #region Дерево навигации

    [Reactive] public TreeDataContext TreeDC { get; set; } = new();

    #endregion

    [Reactive] public bool LeftSubmenuIsChecked { get; set; } = true;
    [Reactive] public bool RightSubmenuIsChecked { get; set; } = true;
    [ObservableAsProperty] public string? LeftSubmenuToolTip { get; }
    [ObservableAsProperty] public string? RightSubmenuToolTip { get; }
    public MainWindowViewModel()
    {
        // Левое подменю
        this.WhenAnyValue(vm => vm.LeftSubmenuIsChecked)
            .Select(x => x ? "Свернуть" : "Развернуть")
            .ToPropertyEx(this, vm => vm.LeftSubmenuToolTip);

        // Правое подменю
        this.WhenAnyValue(vm => vm.RightSubmenuIsChecked)
            .Select(x => x ? "Свернуть" : "Развернуть")
            .ToPropertyEx(this, vm => vm.RightSubmenuToolTip);


        // Выбранная ветка страниц в зависимости от TabItems
        /*this.WhenAnyValue(vm => vm.SelectedTab, vm => vm.Nodes, (index, nodes) => nodes[index])
            .ToPropertyEx(this, vm => vm.SelectedTabNodes);*/
    }
}
/*public class FolderCollection
{
    public ObservableCollection<HandbookFolder> Nodes { get; set; } = [];
    public void Add(string title) => Nodes.Add(new(title));
    public HandbookFolder this[int index] => Nodes[index];
    public HandbookFolder this[string title] => Nodes.Single(x => x.Title == title);
}
public class TreeDataContext : ViewModelBase
{
    *//*public void CreateTree()
    {
        switch (Treetop.Title)
        {
            case "Справочник":
                ((FolderNode)Treetop).Add("Классы");
                List<string> ClassNames = ["Бард", "Варвар", "Воин", "Волшебник", "Друид", "Жрец", "Колдун", "Монах", "Паладин", "Плут", "Следопыт", "Чародей"];
                foreach (string name in ClassNames)
                    ((FolderNode)((FolderNode)Treetop)["Классы"]).Add(new HandbookItem(name));

                break;


            case "Персонажи":
                List<string> CharacterNames = ["Кай", "Кас", "Люциан", "Афелий", "Сол", "Пайк", "Арчи", "Хада Джин", "Владимир", "Джерико Свейн", "Анахорус", "Найло"];
                foreach (string name in CharacterNames)
                    ((FolderNode)Treetop).Add(new CharacterSheet(name));

                break;


            case "Приключения":
                ((FolderNode)Treetop).Add("Проклятие Страда");
                List<string> AdventureNames = ["Жители Баровии", "Гадание", "Характер Страда", "Деревня Баровия", "Валлаки", "Креск", "Ветряная мельница", "Янтарный храм", "Безумный маг", "Ириена Коляна"];
                foreach (string name in AdventureNames)
                    ((FolderNode)((FolderNode)Treetop)["Проклятие Страда"]).Add(new AdventureNote(name));

                ((FolderNode)Treetop).Add("Низвержение в Авернус");

                ((FolderNode)Treetop).Add("А кто, все-таки, главный?");

                break;
        }
    }*//*
    [Reactive] FolderCollection HandbookTree { get; set; } = new();
    [Reactive] ObservableCollection<CharacterNode> CharactersTree { get; set; } = [];
    [Reactive] ObservableCollection<AdventuresFolder> AdventuresTree { get; set; } = [];
    [Reactive] object Treetop { get; set; }
    [Reactive] public object SelectedNode { get; set; }
    public TreeDataContext()
    {
        HandbookTree.Add("Классы");
        HandbookTree[0].Add("Бард");
        HandbookTree[0].Add("Варвар");
        HandbookTree[0].Add("Воин");
        HandbookTree[0].Add("Волшебник");

        Treetop = HandbookTree;
        SelectedNode = Treetop;
    }
}*/
public class TreeTop : ObservableCollection<Node> { }
public class TreeDataContext : ViewModelBase
{
    public Node HandbookTreeGenerate()
    {
        Treetop = new FolderNode("Справочник");

        List<string> HandbookTitles = ["Классы", "Расы", "Черты", "Предыстории", "Снаряжение", "Оружие", "Доспехи"];

        Dictionary<string, List<string>> HandbookContents = new()
        {
            ["Классы"] = ["Бард", "Варвар", "Воин", "Волшебник"],
        };
        foreach (string title in HandbookTitles)
            ((FolderNode)Treetop).Add(title);

        foreach ((string key, List<string> value) in HandbookContents)
            ((FolderNode)((FolderNode)Treetop)[key]).AddRange(value.Select(x => new HandbookItem(x)).ToList());
        
        return Treetop;
    }
    [Reactive] public Node Treetop { get; set; }
    [Reactive] public Node? SelectedNode { get; set; }
    [Reactive] public Node Content { get; set; }
    [Reactive] public Node? SelectedContent { get; set; }

    public TreeDataContext()
    {
        Treetop = HandbookTreeGenerate();

        this.WhenAnyValue(vm => vm.Content)
            .Where(content => content == null)
            .Subscribe(_ => { Content = Treetop; });
        
        this.WhenAnyValue(vm => vm.SelectedContent)
            .Where(content => content != null)
            .Subscribe(_ => {
                SelectedNode = SelectedContent;
                Content = SelectedContent;
            });
        
        this.WhenAnyValue(vm => vm.SelectedNode)
            .Where(content => content != null)
            .Subscribe(_ => {
                Content = SelectedNode;
            });
    }
}