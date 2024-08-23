using System.ComponentModel;
using Product = Domain.Entities.Product.ProductModel;
using System.Runtime.CompilerServices;
using DesktopUI.Utilities.Services;
using static DesktopUI.Utilities.Services.ProductService;
using Model = DesktopUI.Models.ProductModel;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace DesktopUI.ViewModels.Products
{
	public class TableVM : INotifyPropertyChanged
	{
		// Fields
		private List<Product> _products;
		private Model _model;

		// Properties
		public ObservableCollection<Product> Products
		{
			get => new ObservableCollection<Product>(_products);
			set
			{
				_products = value.ToList();
				OnPropertyChanged(nameof(Products));
			}
		}


		// Ctors
        public TableVM()
        {
			_model = new Model();
			ProductsChangedEvent += OnProductsChanged;
			Debug.WriteLine("TableVM initialized");
			Init();
		}

		// Commands



		// Public methods

		


		// Private methods

		private void OnProductsChanged(IList<Product> products)
		{
			_products = products.ToList();
		}

		private async void Init()
		{
			SetCurrentList(await _model.GetLast());
		}

		// INotifyPropertyChanged realization

		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
