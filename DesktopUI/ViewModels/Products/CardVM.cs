using Model = DesktopUI.Models.ProductModel;
using Product = Domain.Entities.Product.ProductModel;
using System.Diagnostics;
using DesktopUI.Utilities;
using System.Collections.ObjectModel;
using Domain.Entities.Product;
using DesktopUI.Utilities.Services;
using System.Windows.Input;
using DesktopUI.Commands;
using System.Windows.Media;

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
		private ObservableCollection<ColorVM> _colors;
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
		public ObservableCollection<ColorVM> Colors
		{
			get => _colors;
			set
			{
				_colors = value;
				OnPropertyChanged(nameof(Colors));
			}
		}
		public TechSpec SelectedTechSpec
		{
			get => _selectedTechSpec;
			set
			{
				_selectedTechSpec = value;
				OnPropertyChanged(nameof(SelectedTechSpec));
				Colors = ConvertToColorVMs(_selectedTechSpec.Colors);
			}
		}
		public ProductColor SelectedColor
		{
			get => _selectedColor;
			set
			{
				if(_selectedColor != null && _selectedColor.Equals(value)) return;
				_selectedColor = value;
				OnPropertyChanged(nameof(SelectedColor));
				CheckQuantity();
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
			Initialize();
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
		public ICommand UpdateColorsCommand { get; set; }
		public ICommand AddSizeCommand { get; set; }
		public ICommand UpdateSizeCommand { get; set; }
		public ICommand DeleteSizeCommand { get; set; }
		public ICommand UpdateSizesCommand { get; set; }
		private async void AddTechSpec(object parameter)
		{
			await _model.AddTechSpec(_product);
			OnPropertyChanged(nameof(TechSpecs));
		}
		public async void CopyTechSpec(object parameter)
		{
			await _model.AddTechSpec(_product, SelectedTechSpec);
		}
		public async void AddColor(object parameter)
		{
			var newColor = GenerateNewColor();
			newColor.TechSpecId = SelectedTechSpec.Id;
			if (await _model.AddColor(newColor))
			{
				newColor.TechSpec = SelectedTechSpec;
				SelectedColor = newColor;
				SelectedSize = null;
				var newColorList = SelectedTechSpec.Colors;
				SelectedTechSpec.Colors = newColorList;
				OnPropertyChanged(nameof(TechSpecs));
			}
		}
		public async void UpdateColor(object parameter)
		{
			await _model.UpdateColor(SelectedColor);
		}
		public async void DeleteColor(object parameter)
		{
			await _model.DeleteColor(SelectedColor);
		}
		public async void UpdateColors(object parameter)
		{
			foreach(var color in SelectedTechSpec.Colors)
			{
				await _model.UpdateColor(color);
			}
		}
		public async void AddSize(object parameter)
		{
			var newSize = GenerateNewSize();
			newSize.ColorId = SelectedColor.Id;
			if (await _model.AddSize(newSize))
			{
				newSize.Color = SelectedColor;
				var newSizeArray = SelectedColor.Sizes;
				newSizeArray.Add(newSize);
				SelectedColor.Sizes = newSizeArray;
				OnPropertyChanged(nameof(SelectedColor));
			}
		}
		public async void UpdateSize(object parameter)
		{
			await _model.UpdateSize(SelectedSize);
		}
		public async void DeleteSize(object parameter)
		{
			await _model.DeleteSize(SelectedSize);
		}
		public async void UpdateSizes(object parameter)
		{
			foreach(var size in SelectedColor.Sizes)
			{
				await _model.UpdateSize(size);
			}
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
		private ObservableCollection<ColorVM> ConvertToColorVMs(List<ProductColor> colors)
		{
			var result = new ObservableCollection<ColorVM>();
			foreach (var color in colors)
			{
				result.Add(new ColorVM(color, _model));
			}
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
			UpdateSizesCommand = new RelayCommand(UpdateSizes);
			UpdateColorsCommand = new RelayCommand(UpdateColors);
		}
		private void Initialize()
		{
			if(_model == null) _model = new Model();
			_product = ProductService.Current;
		}
		private void CheckQuantity()
		{
			int summ = 0;
			if (SelectedColor == null) return;
			foreach(var size in SelectedColor.Sizes)
			{
				summ += size.Quantity;
			}
			if(SelectedColor.TotalQuantity != summ)
			{
				SelectedColor.TotalQuantity = summ;
				UpdateColor(SelectedColor);
			}
		}
	}
}
