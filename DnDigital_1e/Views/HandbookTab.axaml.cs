using Avalonia.Controls;
using DnDigital_1e.ViewModels;

namespace DnDigital_1e.Views
{
    public partial class HandbookTab : UserControl
    {
        public HandbookTab()
        {
            InitializeComponent();
            DataContext = new HandbookTabViewModel();
        }
    }
}
