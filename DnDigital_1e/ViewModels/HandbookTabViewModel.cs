using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using System.Linq;
using System.Collections.ObjectModel;

namespace DnDigital_1e.ViewModels
{
    public class HandbookTabViewModel : MainWindowViewModel
    {
        public class CharacterClass(string name, string engName, byte hits, string source) : ReactiveObject
        {
            private readonly byte _hits = hits;
            public string Name { get; set; } = name;
            public string EngName { get; set; } = engName;
            public string Hits => "к" + _hits;
            public string Source { get; set; } = source;
            [Reactive] public bool IsChecked { get; set; }
        }
        public ObservableCollection<CharacterClass> ItemsCollection { get; set; } = [
                new CharacterClass("Бард",      "Bard",         8,  "PHB"),
                new CharacterClass("Варвар",    "Barbarian",    12, "PHB"),
                new CharacterClass("Воин",      "Fihter",       10, "PHB"),
                new CharacterClass("Волшебник", "Wizard",       6,  "PHB")
            ];
        [ObservableAsProperty] public bool IsAnyChecked { get; }
        [ObservableAsProperty] public int ColumnSpanIfChecked { get; }

        public HandbookTabViewModel()
        {
            this.WhenAnyValue(vm => vm.ItemsCollection)
                .Select(items => items.Any(item => item.IsChecked))
                .ToPropertyEx(this, vm => vm.IsAnyChecked);

            this.WhenAnyValue(vm => vm.ItemsCollection)
                .Select(items => items.Any(item => item.IsChecked))
                .Select(x => x ? 1 : 3)
                .ToPropertyEx(this, vm => vm.ColumnSpanIfChecked);
        }
    }
}