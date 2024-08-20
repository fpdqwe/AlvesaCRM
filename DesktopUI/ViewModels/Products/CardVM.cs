using DesktopUI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesktopUI.ViewModels.Products
{
	public class CardVM : INotifyPropertyChanged
	{
        private ProductModel _product;

        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
