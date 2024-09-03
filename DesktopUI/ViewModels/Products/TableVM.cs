using System.ComponentModel;
using Product = Domain.Entities.Product.ProductModel;
using System.Runtime.CompilerServices;
using DesktopUI.Utilities.Services;
using static DesktopUI.Utilities.Services.ProductService;
using Model = DesktopUI.Models.ProductModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using DesktopUI.Utilities;

namespace DesktopUI.ViewModels.Products
{
	public class TableVM : BaseViewModel
	{
		// Fields
		private static int CallCounter;
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
			//_products = new List<Product>(){
			//	new Product{Id = 1,
			//	CompanyId = AuthService.CurrentUser.CompanyId,
			//	ModelNumber = "test",
			//	Name = "Test",
			//	Description = "Test",
				
			//}
			//};
			Debug.WriteLine("TableVM initialized");
			Init();
		}

		// Commands



		// Public methods




		// Private methods

		private void OnProductsChanged(IList<Product> products)
		{
			if (CompareLists(_products, products.ToList())) 
			{ 
				Debug.WriteLine("Новый список продуктов не отличается от текущего, хотя был вызван ивент ProductsChangedEvent!");
				return; 
			}
			Products = new ObservableCollection<Product>(products);
		}

		private async void Init()
		{
			await _model.GetLast();
		}

		private bool CompareLists(List<Product> list1, List<Product> list2)
		{
			CallCounter++;
			Debug.WriteLine("=======================");
			Debug.WriteLine($"Comparing №{CallCounter} two lists of products!");
			if (list1 == null) return false;
			if(list1.Count != list2.Count) return false;
			for (int i = 0; i <= list1.Count - 1; i++)
			{
				Debug.WriteLine(list1[i].ToString());
				Debug.WriteLine(list2[i].ToString());
				if (!list1[i].Equals(list2[i])) return false;
			}
			return true;
		}
	}
}
