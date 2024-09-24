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
		private TechSpecVM _selectedTechSpec;
		private ColorVM _selectedColor;
		private ObservableCollection<TechSpecVM> _techSpecs;
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
		public ObservableCollection<TechSpecVM> TechSpecs
		{
			get => _techSpecs;
			set
			{
				_techSpecs = value;
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
		public TechSpecVM SelectedTechSpec
		{
			get => _selectedTechSpec;
			set
			{
				_selectedTechSpec = value;
				OnPropertyChanged(nameof(SelectedTechSpec));
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
		// Commands realization
		private async void AddTechSpec(object parameter)
		{
			await _model.AddTechSpec(_product);
			OnPropertyChanged(nameof(TechSpecs));
		}
		public async void CopyTechSpec(object parameter)
		{
			await _model.AddTechSpec(_product, SelectedTechSpec.TechSpec);
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
		private ObservableCollection<ColorVM> ConvertToColorVMs(List<ProductColor> colors)
		{
			var result = new ObservableCollection<ColorVM>();
			foreach (var color in colors)
			{
				result.Add(new ColorVM(color, _model, 1));
			}
			return result;
		}
		private ObservableCollection<TechSpecVM> ConvertToTechSpecVMs(List<TechSpec> techSpecs)
		{
			var result = new ObservableCollection<TechSpecVM>();
			foreach(var techSpec in techSpecs)
			{
				result.Add(new TechSpecVM(techSpec, _model));
			}
			return result;
		}
		private void SetCommands()
		{
			AddTechSpecCommand = new RelayCommand(AddTechSpec);
			CopyTechSpecCommand = new RelayCommand(CopyTechSpec);
		}
		private void Initialize()
		{
			if(_model == null) _model = new Model();
			_product = ProductService.Current;
			TechSpecs = ConvertToTechSpecVMs(_product.TechSpecs);
			SelectedTechSpec = TechSpecs.First();
		}
	}
}
