using System.ComponentModel;
using ReactiveUI;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Reactive;

namespace DnDigital_1e.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Закрытые переменные левого меню
        private bool _profileIsChecked; //хз
        private bool _homePageIsChecked; //хз
        private bool _handBookIsChecked; //меняется
        private bool _charactersIsChecked; //меняется
        private bool _adventuresIsChecked; //меняется
        private bool _logOutIsChecked; //нету не меняетя
        private bool _helpIsChecked; //нету не меняется
        #endregion

        #region Открытые переменные левого меню
        public bool ProfileIsChecked
        {
            get => _profileIsChecked;
            set => this.RaiseAndSetIfChanged(ref _profileIsChecked, value);
        }
        public bool HomePageIsChecked
        {
            get => _homePageIsChecked;
            set => this.RaiseAndSetIfChanged(ref _homePageIsChecked, value);
        }
        public bool HandBookIsChecked
        {
            get => _handBookIsChecked;
            set
            {
                if (value && SubmenuNavigationIsChecked) SubMenuDataContext = new HandbookTabViewModel();
                else SubMenuDataContext = new MainWindowViewModel();
                this.RaiseAndSetIfChanged(ref _handBookIsChecked, value);
            }
        }
        public bool CharactersIsChecked
        {
            get => _charactersIsChecked;
            set
            {
                if (value && SubmenuNavigationIsChecked) SubMenuDataContext = new CharactersTabViewModel();
                else SubMenuDataContext = new MainWindowViewModel();
                this.RaiseAndSetIfChanged(ref _handBookIsChecked, value);
            }
        }
        public bool AdventuresIsChecked
        {
            get => _adventuresIsChecked;
            set
            {
                if (value && SubmenuNavigationIsChecked) SubMenuDataContext = new AdventuresTabViewModel();
                else SubMenuDataContext = new MainWindowViewModel();
                this.RaiseAndSetIfChanged(ref _handBookIsChecked, value);
            }
        }
        public bool LogOutIsChecked
        {
            get => _logOutIsChecked;
            set => this.RaiseAndSetIfChanged(ref _logOutIsChecked, value);
        }
        public bool HelpsIsChecked
        {
            get => _helpIsChecked;
            set => this.RaiseAndSetIfChanged(ref _helpIsChecked, value);
        }
        #endregion

        #region Верхнее левое подменю
        private bool _subMenuNavigationIsChecked;
        public bool SubmenuNavigationIsChecked
        {
            get => _subMenuNavigationIsChecked;
            set => this.RaiseAndSetIfChanged(ref _subMenuNavigationIsChecked, value);
        }
        private object _subMenuDataContext;
        public object SubMenuDataContext
        {
            get => _subMenuDataContext;
            set => this.RaiseAndSetIfChanged(ref _subMenuDataContext, value);
        }
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
        #endregion

        public MainWindowViewModel()
        {
            SubMenuDataContext = new MainWindowViewModel();
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