using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System;
using AvaloniaEdit.Utils;
using Avalonia.Media;
using DnDigital_1e.Models;
using DynamicData;
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

    #region Дерево навигации

    [Reactive] public HandbookDataContext HandbookDC { get; set; } = new();
    /*[Reactive] public CharactersDataContext CharactersDC { get; set; } = new();
    [Reactive] public AdventuresDataContext AdventuresDC { get; set; } = new();*/
    [Reactive] public ITreeViewModel ContentDataContext { get; set; }

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

        ContentDataContext = HandbookDC;

        // Выбранная ветка страниц в зависимости от TabItems
        /*this.WhenAnyValue(vm => vm.SelectedTab, vm => vm.Nodes, (index, nodes) => nodes[index])
            .ToPropertyEx(this, vm => vm.SelectedTabNodes);*/
    }
}
public interface ITreeViewModel
{
    [Reactive] public Node<INodeContent> Treetop { get; set; }
}
public class HandbookDataContext : ITreeViewModel
{
    [Reactive] public Node<INodeContent> Treetop { get; set; } = new("Справочик", new NodeFolder());
    public HandbookDataContext()
    {
        ((NodeFolder)Treetop.Content).Add(new("Классы", new NodeFolder()));
    }
}
/*public class CharactersDataContext : ITreeViewModel
{
    [Reactive] public FolderNode Treetop { get; set; } = new FolderNode("Персонажи");
    public CharactersDataContext()
    {
        Treetop.Add("Гоблины");
        Treetop.Add("Плуты");
    }
}
public class AdventuresDataContext : ITreeViewModel
{
    [Reactive] public FolderNode Treetop { get; set; } = new FolderNode("Приключения");
    public AdventuresDataContext()
    {
        Treetop.Add("Проклятие Страда");
        Treetop.Add("Низвержение в Авернус");
    }
}*/