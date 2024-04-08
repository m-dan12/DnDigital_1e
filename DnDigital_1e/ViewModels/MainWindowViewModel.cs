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

        private bool _leftSubmenuIsChecked;
        /*private bool _leftSubmenuToolTipIsVisible = true;*/
        public string LeftSubmenuToolTip => _leftSubmenuIsChecked ? "Свернуть" : "Развернуть";
        public bool LeftSubmenuIsChecked
        {
            get => _leftSubmenuIsChecked;
            set
            {
                _leftSubmenuIsChecked = value;
                /*LeftSubmenuToolTipIsVisible = false;*/
                OnPropertyChanged("LeftSubmenuIsChecked");
                OnPropertyChanged("LeftSubmenuToolTip");
            }
        }
        /*public bool LeftSubmenuToolTipIsVisible
        {
            get => _leftSubmenuToolTipIsVisible;
            set => this.RaiseAndSetIfChanged(ref _leftSubmenuToolTipIsVisible, value);
        }

        // ЕБАЛ Я В СРАКУ ЭТИ СОБЫТИЯ ПОИНТЕР ЭНТЕРЕД
        private ReactiveCommand<Unit, Unit> _toggleButtonPointerEnterCommand;
        public ReactiveCommand<Unit, Unit> ToggleButtonPointerEnterCommand => _toggleButtonPointerEnterCommand ??= ReactiveCommand.Create(ShowToolTip);
        public void ShowToolTip() => LeftSubmenuToolTipIsVisible = true;*/


        private bool _rightSubmenuIsChecked;
        public bool RightSubmenuIsChecked
        {
            get => _rightSubmenuIsChecked;
            set
            {
                _rightSubmenuIsChecked = value;
                OnPropertyChanged("RightSubmenuIsChecked");
                OnPropertyChanged("RightSubmenuToolTip");
            }
        }
        public string RightSubmenuToolTip => _rightSubmenuIsChecked ? "Свернуть" : "Развернуть";

    }
}