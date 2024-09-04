using Model = DesktopUI.Models.ProductModel;
using Product = Domain.Entities.Product.ProductModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DesktopUI.Utilities;
using System.Collections.ObjectModel;
using Domain.Entities.Product;
using DesktopUI.Utilities.Services;

namespace DesktopUI.ViewModels.Products
{
	public class CardVM : BaseViewModel
	{
		// Fields
        private Model _model;
		private Product _product;

		//Properties
		public string Name
		{
			get => _product.Name;
			set
			{
				_product.Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string ModelNumber
		{
			get => _product.ModelNumber;
			set
			{
				_product.ModelNumber = value;
				OnPropertyChanged(nameof(ModelNumber));
			}
		}
		public string Description
		{
			get => _product.Description;
			set
			{
				_product.Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}
		public ObservableCollection<TechSpec> TechSpecs
		{
			get => new ObservableCollection<TechSpec>(_product.TechSpecs);
			set
			{
				_product.TechSpecs = value.ToList();
				OnPropertyChanged(nameof(TechSpecs));
			}
		}

		// Ctors
        public CardVM()
        {
			_model = new Model();
			_product = ProductService.Current;
			Debug.WriteLine("CardVM initialized");
		}
		public CardVM(Product product)
		{
			_model = new Model();
			_product = product;
		}

		// Commands


		// Private methods
	}
}
