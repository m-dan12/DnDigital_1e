using System.ComponentModel;
using System.Data.Common;
using System.Reactive.Linq;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls.Primitives;
using Avalonia.Controls;
using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Reactive;
using Avalonia.Input;

namespace DnDigital_1e.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public class SubmenuItem : ReactiveObject
        {
            private string _name;
            private string _icon;
            public string Name
            {
                get => _name;
                set => this.RaiseAndSetIfChanged(ref _name, value);
            }
            public string Icon
            {
                get => _icon;
                set => this.RaiseAndSetIfChanged(ref _icon, value);
            }
            public SubmenuItem(string name, string icon)
            {
                _name = name;
                _icon = icon;
            }
        }
        public ObservableCollection<SubmenuItem> SubmenuItems { get; set; } = [];
        public MainWindowViewModel()
        {

        }
            



        #region Объявление команд

        private ReactiveCommand<Unit, Unit> _closeCommand;
        private ReactiveCommand<Unit, Unit> _hideCommand;
        private ReactiveCommand<Unit, Unit> _deployCommand;

        #endregion



        #region Инициализация команд

        public ReactiveCommand<Unit, Unit> CloseCommand =>
            _closeCommand ?? (_closeCommand = ReactiveCommand.Create(CloseWindow));
        public ReactiveCommand<Unit, Unit> HideCommand =>
            _hideCommand ?? (_hideCommand = ReactiveCommand.Create(HideWindow));
        public ReactiveCommand<Unit, Unit> DeployCommand =>
            _deployCommand ?? (_deployCommand = ReactiveCommand.Create(DeployWindow));

        #endregion



        #region Методы

        private void CloseWindow()
        {
            var window = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            window.Close();
        }
        private void HideWindow()
        {
            var window = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            window.WindowState = WindowState.Minimized;
        }
        private void DeployWindow()
        {
            var window = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        #endregion



        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}