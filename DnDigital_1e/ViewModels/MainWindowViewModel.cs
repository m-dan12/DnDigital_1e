using System.ComponentModel;
using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Reactive;
using Avalonia.Input;

namespace DnDigital_1e.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {



        #region Объявление команд

        private ReactiveCommand<Unit, Unit> _closeCommand;
        private ReactiveCommand<Unit, Unit> _hideCommand;
        private ReactiveCommand<Unit, Unit> _expandCommand;

        #endregion



        #region Инициализация команд

        public ReactiveCommand<Unit, Unit> CloseCommand =>
            _closeCommand ?? (_closeCommand = ReactiveCommand.Create(CloseWindow));
        public ReactiveCommand<Unit, Unit> HideCommand =>
            _hideCommand ?? (_hideCommand = ReactiveCommand.Create(HideWindow));
        public ReactiveCommand<Unit, Unit> ExpandCommand =>
            _expandCommand ?? (_expandCommand = ReactiveCommand.Create(ExpandWindow));

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
        private void ExpandWindow()
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