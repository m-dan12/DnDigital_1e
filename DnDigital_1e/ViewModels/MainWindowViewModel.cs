using DnDigital_1e.Views;
using System.ComponentModel;
using System.Data.Common;
using System.Reactive.Linq;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls.Primitives;
using Avalonia.Controls;

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
    }
}