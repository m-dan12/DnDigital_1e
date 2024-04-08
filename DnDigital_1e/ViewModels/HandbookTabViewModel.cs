using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace DnDigital_1e.ViewModels
{
    public class HandbookTabViewModel : MainWindowViewModel
    {
        public class CharacterClass(string name, string engName, byte hits, string source) : ReactiveObject
        {
            private byte _hits = hits;
            private bool _isChecked = false;
            public string Name { get; set; } = name;
            public string EngName { get; set; } = engName;
            public string Hits => "к" + _hits;
            public string Source { get; set; } = source;
            public bool IsChecked
            {
                get => _isChecked;
                set => this.RaiseAndSetIfChanged(ref _isChecked, value);
            }
        }
        public ObservableCollection<CharacterClass> ItemsCollection { get; set; }
        public bool IsAnyChecked => ItemsCollection.Any(x => x.IsChecked);
        public int ColumnSpanIfChecked => IsAnyChecked ? 1 : 3;

        public HandbookTabViewModel()
        {
            ItemsCollection = [
                new CharacterClass("Бард", "Bard", 8, "PHB"),
                new CharacterClass("Варвар", "Barbarian", 12, "PHB"),
                new CharacterClass("Воин", "Fihter", 10, "PHB"),
                new CharacterClass("Волшебник", "Wizard", 6, "PHB")
            ];
            foreach (var item in ItemsCollection)
                item.PropertyChanged += (sender, args) => {
                    if (args.PropertyName == nameof(CharacterClass.IsChecked))
                    {
                        OnPropertyChanged(nameof(IsAnyChecked));
                        OnPropertyChanged(nameof(ColumnSpanIfChecked));
                    }
                };
        }
    }
}