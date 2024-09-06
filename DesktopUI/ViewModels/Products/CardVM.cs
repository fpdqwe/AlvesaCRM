using Model = DesktopUI.Models.ProductModel;
using Product = Domain.Entities.Product.ProductModel;
using System.Diagnostics;
using DesktopUI.Utilities;
using System.Collections.ObjectModel;
using Domain.Entities.Product;
using DesktopUI.Utilities.Services;
using System.Windows.Input;
using DesktopUI.Commands;

namespace DesktopUI.ViewModels.Products
{
	public class CardVM : BaseViewModel
	{
		// Fields
        private Model _model;
		private Product _product;
		private TechSpec _selectedTechSpec;
		private ProductColor _selectedColor;
		private ProductSize _selectedSize;
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
		public TechSpec SelectedTechSpec
		{
			get => _selectedTechSpec;
			set
			{
				_selectedTechSpec = value;
				OnPropertyChanged(nameof(SelectedTechSpec));
			}
		}
		public ProductColor SelectedColor
		{
			get => _selectedColor;
			set
			{
				_selectedColor = value;
				OnPropertyChanged(nameof(SelectedColor));
			}
		}
		public ProductSize SelectedSize
		{
			get => _selectedSize;
			set
			{
				_selectedSize = value;
				OnPropertyChanged(nameof(SelectedSize));
			}
		}

		// Ctors
        public CardVM()
        {
			_model = new Model();
			_product = ProductService.Current;
			Debug.WriteLine("CardVM initialized");
			ProductService.CurrentChangedEvent += OnCurrentChanged;
			
			SetCommands();
		}
		public CardVM(Product product)
		{
			_model = new Model();
			_product = product;
			ProductService.CurrentChangedEvent += OnCurrentChanged;

			SetCommands();
		}

		// Commands
		public ICommand AddTechSpecCommand { get; set; }
		public ICommand CopyTechSpecCommand { get; set; }
		public ICommand AddColorCommand { get; set; }
		public ICommand UpdateColorCommand { get; set; }
		public ICommand DeleteColorCommand { get; set; }
		public ICommand AddSizeCommand { get; set; }
		public ICommand UpdateSizeCommand { get; set; }
		public ICommand DeleteSizeCommand { get; set; }
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
		public void CopyTechSpec(object parameter)
		{
			
		}
		public async void AddColor(object parameter)
		{
			var newColor = GenerateNewColor();
			newColor.TechSpec = SelectedTechSpec;
			await _model.AddColor(newColor);
			SelectedColor = newColor;
			SelectedSize = null;
		}
		public async void UpdateColor(object parameter)
		{
			await _model.UpdateColor(SelectedColor);
		}
		public async void DeleteColor(object parameter)
		{
			await _model.DeleteColor(SelectedColor);
		}
		public async void AddSize(object parameter)
		{
			var newSize = GenerateNewSize();
			newSize.Color = SelectedColor;
			var newSizeArray = SelectedColor.Sizes;
			newSizeArray.Add(newSize);
			SelectedColor.Sizes = newSizeArray;
			await _model.AddSize(newSize);
		}
		public async void UpdateSize(object parameter)
		{
			await _model.UpdateSize(SelectedSize);
		}
		public async void DeleteSize(object parameter)
		{
			await _model.DeleteSize(SelectedSize);
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

		private ProductColor GenerateNewColor()
		{
			var result = new ProductColor() {
				Name = string.Empty,
				TotalQuantity = 0,
				Sizes = new List<ProductSize> { GenerateNewSize() },
				Composition = string.Empty
			};
			return result;
		}
		private ProductSize GenerateNewSize()
		{
			var result = new ProductSize()
			{
				Name = string.Empty,
				Barcode = string.Empty,
				Quantity = 0,
			};
			return result;
		}
		private void SetCommands()
		{
			AddTechSpecCommand = new RelayCommand(AddTechSpec);
			CopyTechSpecCommand = new RelayCommand(CopyTechSpec);

			AddColorCommand = new RelayCommand(AddColor);
			UpdateColorCommand = new RelayCommand(UpdateColor);
			DeleteColorCommand = new RelayCommand(DeleteColor);
			AddSizeCommand = new RelayCommand(AddSize);
			UpdateSizeCommand = new RelayCommand(UpdateSize);
			DeleteSizeCommand = new RelayCommand(DeleteSize);
		}
	}
}
