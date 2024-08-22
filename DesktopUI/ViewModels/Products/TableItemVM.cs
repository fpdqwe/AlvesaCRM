using DesktopUI.Commands;
using DesktopUI.Utilities.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Product = Domain.Entities.Product.ProductModel;

namespace DesktopUI.ViewModels.Products
{
	/// <summary>
	/// Represents a single item in the table of product
	/// </summary>
	public class TableItemVM : INotifyPropertyChanged
	{
		private Product _product;
		public Product Product
		{
			get => _product;
			set
			{
				_product = value;
				OnPropertyChanged(nameof(Product));
			}
		}
        public TableItemVM()
        {
			_product = new Product();
			SelectCommand = new RelayCommand(Select);
			Debug.WriteLine("TableItemVM initialized");
		}
		public TableItemVM(Product product)
		{
			_product = product;
			SelectCommand = new RelayCommand(Select);
			Debug.WriteLine("TableItemVM initialized");
		}

		public ICommand SelectCommand { get; set; }

		private void Select(object obj)
		{
			ProductService.SetCurrent(_product);
		}

        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
