using DesktopUI.Models;
using Product = Domain.Entities.Product;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using DesktopUI.Commands;

namespace DesktopUI.ViewModels
{
	public class ProductVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private ObservableCollection<Product.ProductModel> _products;
		private ProductModel _productModel;

		public ObservableCollection<Product.ProductModel> ProductModels
		{
			get => _products;
			set
			{
				_products = value;
				OnPropertyChanged(nameof(ProductModels));
			}
		}
        public ProductVM()
        {
            _productModel = new ProductModel();
			_products = new ObservableCollection<Product.ProductModel>(_productModel.ProductRepository.GetLast(20).Result);

			AddProductCommand = new RelayCommand(AddProduct);
			UpdateProductCommand = new RelayCommand(UpdateProduct);
			DeleteProductCommand = new RelayCommand(DeleteProduct);
        }
		
		public ICommand AddProductCommand {  get; set; }
		public ICommand UpdateProductCommand { get; set; }
		public ICommand DeleteProductCommand { get; set;}
		public ICommand ReadProductCommand { get; set; }
		public ICommand AddTechSpecCommand {  get; set; }
		public ICommand UpdateTechSpecCommand { get; set; }
		public ICommand DeleteTechSpecCommand { get; set; }
		public ICommand AddColorCommand { get; set; }
		public ICommand UpdateColorCommand { get; set; }
		public ICommand DeleteColorCommand { get; set; }
		public ICommand AddSizeCommand { get; set; }
		public ICommand UpdateSizeCommand { get; set;}
		public ICommand DeleteSizeCommand { get; set; }

		public void AddProduct(object prop)
		{

		}
		public void UpdateProduct(object prop)
		{

		}
		public void DeleteProduct(object prop)
		{

		}

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
