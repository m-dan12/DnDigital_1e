using System.ComponentModel;
using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Reactive;
using Avalonia.Input;
using System.Collections.ObjectModel;

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
        public ReactiveCommand<Unit, Unit> CloseCommand => _closeCommand ??= ReactiveCommand.Create(CloseWindow);
        public ReactiveCommand<Unit, Unit> HideCommand => _hideCommand ??= ReactiveCommand.Create(HideWindow);
        public ReactiveCommand<Unit, Unit> ExpandCommand => _expandCommand ??= ReactiveCommand.Create(ExpandWindow);

        // Методы
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
        private void ExpandWindow()
        {
            var window = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        #endregion

        private bool _leftSubMenuIsChecked;
        public bool LeftSubMenuIsChecked
        {
            get => _leftSubMenuIsChecked;
            set => this.RaiseAndSetIfChanged(ref _leftSubMenuIsChecked, value);
        }
        private string _leftSubMenuToolTip;
        public string LeftSubMenuToolTip
        {
            get => _leftSubMenuToolTip;
            set => this.RaiseAndSetIfChanged(ref _leftSubMenuToolTip, value);
        }
        private bool _rightSubMenuIsChecked;
        public bool RightSubMenuIsChecked
        {
            get => _rightSubMenuIsChecked;
            set => this.RaiseAndSetIfChanged(ref _rightSubMenuIsChecked, value);
        }

    }
}