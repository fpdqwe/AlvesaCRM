using DesktopUI.Utilities;
using Domain.Entities.Product;
using System.Collections.ObjectModel;

namespace DesktopUI.ViewModels.Products
{
    public class ColorVM : BaseViewModel
    {
        private ObservableCollection<ProductColor> _colors;

        public ObservableCollection<ProductColor> Colors
        {
            get => _colors;
            set
            {
                _colors = value;
                OnPropertyChanged(nameof(Colors));
            }
        }
    }
}
