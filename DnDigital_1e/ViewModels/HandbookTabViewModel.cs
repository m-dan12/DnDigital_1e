using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using System.Linq;
using System.Collections.ObjectModel;
using Svg;

namespace DnDigital_1e.ViewModels
{
    public class HandbookTabViewModel : MainWindowViewModel
    {

        public ObservableCollection<Node> SelectedNodes { get; set; } = [
             new Node("Классы", [ new ("Бард", "Bard", "PHB", 8), new ("Варвар", "Barbarian", "PHB", 12), new ("Воин", "Fighter", "PHB", 10), new ("Волшебник", "Wizard", "PHB", 6) ]),
        ];
        [Reactive] public bool IsAnyChecked { get; set; } = false;
        [Reactive] public int ColumnSpanIfChecked { get; set; } = 3;

        public HandbookTabViewModel()
        {


            this.WhenAnyValue(vm => vm.SelectedNodes)
                .SelectMany(collection => collection.Select(item => item.WhenAnyValue(x => x.IsSelected)))
                .Merge()
                .Select(_ => SelectedNodes.Any(item => item.IsSelected))
                .ToPropertyEx(this, vm => vm.IsAnyChecked);

            this.WhenAnyValue(vm => vm.IsAnyChecked)
                .Select(x => x ? 1 : 3)
                .ToPropertyEx(this, vm => vm.ColumnSpanIfChecked);
        }
    }
}