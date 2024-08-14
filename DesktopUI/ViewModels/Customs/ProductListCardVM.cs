using DesktopUI.Models;
using System.ComponentModel;

namespace DesktopUI.ViewModels.Customs
{
	public class ProductListCardVM : INotifyPropertyChanged
	{
        private ProductModel _product;

        public event PropertyChangedEventHandler? PropertyChanged;
	}
}
