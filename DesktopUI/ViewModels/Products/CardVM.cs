using Model = DesktopUI.Models.ProductModel;
using Product = Domain.Entities.Product.ProductModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DesktopUI.Utilities;
using System.Collections.ObjectModel;
using Domain.Entities.Product;
using DesktopUI.Utilities.Services;
using System.Windows.Input;

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
			ProductService.CurrentChangedEvent += OnCurrentChanged;
		}
		public CardVM(Product product)
		{
			_model = new Model();
			_product = product;
			ProductService.CurrentChangedEvent += OnCurrentChanged;
		}

		// Commands
		public ICommand AddTechSpecCommand { get; set; }

		private void AddTechSpec(object parameter)
		{
			ProductSize size = new ProductSize();
			ProductColor color = new ProductColor();
			color.Sizes = new List<ProductSize>() { size };
			size.Color = color;
			size.ColorId = color.Id;
			TechSpec techSpec = new TechSpec();
			techSpec.Colors = new List<ProductColor>() { color };
			color.TechSpec = techSpec;
			color.TechSpecId = techSpec.Id;
			
		}

		// Private methods
		private void OnCurrentChanged(Product product)
		{
			_product = product;
			OnPropertyChanged(nameof(TechSpecs));
			OnPropertyChanged(nameof(Name));
			OnPropertyChanged(nameof(Description));
			OnPropertyChanged(nameof(ModelNumber));
		}
	}
}
