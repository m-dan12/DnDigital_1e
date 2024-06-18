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


namespace DnDigital_1e.ViewModels
{
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

        public class Node : ReactiveObject
        {
            public ObservableCollection<Node>? SubNodes { get; }
            [Reactive] public bool IsSelected { get; set; } = false;
            [Reactive] public string Title { get; set; }

            #region Справочные штучки
            public string? EngTitle { get; }
            public string? Source { get; }
            private byte? _hitDice { get; }
            public string? HitDice => _hitDice != null ? "к" + _hitDice : null;
            #endregion

            public Node(string title) => Title = title;
            public Node(string title, ObservableCollection<Node> subNodes)
            {
                Title = title;
                SubNodes = subNodes;
            }
            public Node(string title, string engTitle, string source) // Справочник
            {
                Title = title;
                EngTitle = engTitle;
                Source = source;
            }
            public Node(string title, string engTitle, string source, byte hitDice) // Справочник -> Классы
            {
                Title = title;
                EngTitle = engTitle;
                Source = source;
                _hitDice = hitDice;
            }
        }
        public ObservableCollection<Node> Nodes { get; } = [
                new ("Главная"),
                new ("Справочник", [
                        new ("Классы", [ new ("Бард", "Bard", "PHB", 8), new ("Варвар", "Barbarian", "PHB", 12), new ("Воин", "Fighter", "PHB", 10), new ("Волшебник", "Wizard", "PHB", 6) ]),
                        new ("Расы", [ new ("Ааракокра"), new ("Аасимар"), new ("Автогном"), new ("Багбир") ]),
                        new ("Черты", [ new ("Агент порядка"), new ("Агрессия орков"), new ("Адепт Белых одежд"), new ("Адепт Красных одежд") ]),
                        new ("Предыстроии", []),
                        new ("Оружие", []),
                        new ("Доспехи", []),
                        new ("Снаряжение", []),
                        new ("Заклинания", []),
                        new ("Магические предметы", []),
                        new ("Бестиарий", []),
                        new ("Правила", []),
                    ]),
                new ("Персонажи", [
                        new ("Гоблины", [ new ("Боблин"), new ("Воблин"), new ("Моблин") ]),
                        new ("Кай"),
                        new ("Кас")
                    ]),
                new ("Приключения", [
                        new ("Course of Strahd", [
                                new ("Баровия", [ new ("Ирина Коляна"), new ("Таверна"), new ("Магазин") ]),
                                new ("Случайные столкновения")
                            ])
                    ])
            ];
        
        [Reactive] public int SelectedTab { get; set; } = 0;
        [ObservableAsProperty] public Node? SelectedTabNodes { get; }

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
            this.WhenAnyValue(vm => vm.SelectedTab, vm => vm.Nodes, (index, nodes) => nodes[index])
                .ToPropertyEx(this, vm => vm.SelectedTabNodes);
        }

    }
}