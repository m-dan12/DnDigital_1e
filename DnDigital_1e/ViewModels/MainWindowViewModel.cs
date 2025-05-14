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
using System.Net;
using Avalonia.Data;


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

    #region Кнопки навигации

    [Reactive] public ToggleButtonDataContext ToggleButtonDC { get; set; }

    #endregion

    #region Дерево навигации

    [Reactive] public HomePageDataContext HomePageDC { get; set; } = new();
    [Reactive] public HandbookDataContext HandbookDC { get; set; } = new();
    [Reactive] public CharactersDataContext CharactersDC { get; set; } = new();
    [Reactive] public AdventuresDataContext AdventuresDC { get; set; } = new();
    [Reactive] public TreeDataContext TreeDC { get; set; }

    #endregion

    [Reactive] public bool LeftSubmenuIsChecked { get; set; } = true;
    [Reactive] public bool RightSubmenuIsChecked { get; set; } = true;
    [ObservableAsProperty] public string? LeftSubmenuToolTip { get; }
    [ObservableAsProperty] public string? RightSubmenuToolTip { get; }
    public MainWindowViewModel()
    {
        TreeDC = HandbookDC;
        ToggleButtonDC = new(this);

        // Левое подменю
        this.WhenAnyValue(vm => vm.LeftSubmenuIsChecked)
            .Select(x => x ? "Свернуть" : "Развернуть")
            .ToPropertyEx(this, vm => vm.LeftSubmenuToolTip);

        // Правое подменю
        this.WhenAnyValue(vm => vm.RightSubmenuIsChecked)
            .Select(x => x ? "Свернуть" : "Развернуть")
            .ToPropertyEx(this, vm => vm.RightSubmenuToolTip);
    }
}
public abstract class TreeDataContext : ViewModelBase
{
    public abstract Node TreeGenerate();
    [Reactive] public Node Treetop { get; set; }
    [Reactive] public Node? SelectedNode { get; set; }
    [ObservableAsProperty] public Node Content { get; }
    [ObservableAsProperty] public Node? ContentContent { get; }
    [Reactive] public Node? SelectedContent { get; set; }

    public TreeDataContext()
    {
        Treetop = TreeGenerate();

        this.WhenAnyValue(vm => vm.SelectedNode)
            .Select(node => node ?? Treetop)
            .ToPropertyEx(this, vm => vm.Content);

        this.WhenAnyValue(vm => vm.SelectedContent)
            .Where(x => x != null)
            .Subscribe(_ => { SelectedNode = SelectedContent; });


    }
}
public class HomePageDataContext : TreeDataContext
{
    public override Node TreeGenerate()
    {
        return new FolderNode("Домашняя страница");
    }
}
public class HandbookDataContext : TreeDataContext
{
    public override Node TreeGenerate()
    {
        Node TreeTop = new FolderNode("Справочник");

        List<string> HandbookTitles = ["Классы", "Расы", "Черты", "Предыстории", "Снаряжение", "Оружие", "Доспехи"];

        Dictionary<string, List<string>> HandbookContents = new()
        {
            ["Классы"] = ["Бард", "Варвар", "Воин", "Волшебник"],
        };

        foreach (string title in HandbookTitles)
            ((FolderNode)TreeTop).Add(title);

        foreach ((string key, List<string> value) in HandbookContents)
            ((FolderNode)((FolderNode)TreeTop)[key]).AddRange(value.Select(x => new HandbookItem(x)).ToList());

        return TreeTop;
    }
}
public class CharactersDataContext : TreeDataContext
{
    public override Node TreeGenerate()
    {
        Node TreeTop = new FolderNode("Персонажи");

        List<string> CharacterNames = ["Кай", "Кас", "Афелий", "Заск", "Пайк", "Арчи", "Владимир", "Каин"];

        ((FolderNode)TreeTop).AddRange(CharacterNames.Select(x => new CharacterSheet(x)).ToList());

        return TreeTop;
    }
}
public class AdventuresDataContext : TreeDataContext
{
    public override Node TreeGenerate()
    {
        Node TreeTop = new FolderNode("Приключения");

        List<string> AdventureTitles = ["Проклятие Страда", "Низвержение в Авернус"];

        Dictionary<string, List<string>> AdventureContents = new()
        {
            ["Проклятие Страда"] = ["Деревня Баровия", "Валлаки", "Креск", "Янтарный храм", "Страд фон Зарович", "Гадание"],
        };

        foreach (string title in AdventureTitles)
            ((FolderNode)TreeTop).Add(title);

        foreach ((string key, List<string> value) in AdventureContents)
            ((FolderNode)((FolderNode)TreeTop)[key]).AddRange(value.Select(x => new AdventureNote(x)).ToList());

        return TreeTop;
    }
}

public class ToggleButtonDataContext : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowVM;
    [Reactive] public bool HomePageIsSelected { get; set; }
    [Reactive] public bool HandbookIsSelected { get; set; } = true;
    [Reactive] public bool CharactersIsSelected { get; set; }
    [Reactive] public bool AdventuresIsSelected { get; set; }

    public ToggleButtonDataContext(MainWindowViewModel mainWindowVM)
    {
        _mainWindowVM = mainWindowVM;

        this.WhenAnyValue(vm => vm.HomePageIsSelected)
            .Where(x => x)
            .Subscribe(_ =>
            {
                _mainWindowVM.TreeDC = _mainWindowVM.HomePageDC;
                HandbookIsSelected = false;
                CharactersIsSelected = false;
                AdventuresIsSelected = false;
            });
        this.WhenAnyValue(vm => vm.HandbookIsSelected)
            .Where(x => x)
            .Subscribe(_ =>
            {
                _mainWindowVM.TreeDC = _mainWindowVM.HandbookDC;
                HomePageIsSelected = false;
                CharactersIsSelected = false;
                AdventuresIsSelected = false;
            });
        this.WhenAnyValue(vm => vm.CharactersIsSelected)
            .Where(x => x)
            .Subscribe(_ =>
            {
                _mainWindowVM.TreeDC = _mainWindowVM.CharactersDC;
                HomePageIsSelected = false;
                HandbookIsSelected = false;
                AdventuresIsSelected = false;
            });
        this.WhenAnyValue(vm => vm.AdventuresIsSelected)
            .Where(x => x)
            .Subscribe(_ =>
            {
                _mainWindowVM.TreeDC = _mainWindowVM.AdventuresDC;
                HomePageIsSelected = false;
                HandbookIsSelected = false;
                CharactersIsSelected = false;
            });
    }
}