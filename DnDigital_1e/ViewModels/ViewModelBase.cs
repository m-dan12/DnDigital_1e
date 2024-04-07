using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static DnDigital_1e.ViewModels.MainWindowViewModel;

namespace DnDigital_1e.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}