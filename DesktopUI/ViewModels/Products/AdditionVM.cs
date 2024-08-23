using DAL;
using DAL.Repository;
using DesktopUI.Commands;
using DesktopUI.Utilities;
using Domain.Entities.Product;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model = DesktopUI.Models.ProductModel;
using Product = Domain.Entities.Product.ProductModel;

namespace DesktopUI.ViewModels.Products
{
	public class AdditionVM : INotifyPropertyChanged
	{
		// Fields and properties
		private const string UNASSIGNED = "UNASSIGNED";
		private const string ADDING_ERROR = "Что-то пошло не так, модель не добавлена...";
		private const string ADDING_SUCCESS = "Модель успешно добавлена в систему.";
		private Product _product;
		private string _message;
		private Model _model;
		public Product Product
		{
			get => _product;
			set
			{
				_product = value;
				OnPropertyChanged(nameof(Product));
			}
		}
		public string Message
		{
			get => _message;
			set
			{
				_message = value;
				OnPropertyChanged(nameof(Message));
			}
		}

		// Ctors
        public AdditionVM()
        {
			_product = new Product();
			_model = new Model();
			Debug.WriteLine("AddtionVM initialized");
			CreateCommand = new RelayCommand(CreateNewProduct);
        }

		// Commands
		public ICommand CreateCommand { get; set; }

		private async void CreateNewProduct(object obj)
		{
			if(Product == null) { return;}
			Product.CompanyId = AuthService.CurrentUser.CompanyId;
			Product.TechSpecs = new List<TechSpec>() { CreateEmptyTS() };
			if (await _model.Add(Product)) Message = ADDING_ERROR;
			else Message = ADDING_SUCCESS;
		}

		// Public methods


		// INotifyPropertyChanged realization
        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		// Private methods
		private TechSpec CreateEmptyTS()
		{
			var size = new ProductSize()
			{
				Name = UNASSIGNED,
				Quantity = 0
			};
			var color = new ProductColor()
			{
				Name = UNASSIGNED,
				Sizes = new List<ProductSize>() { size},
				TotalQuantity = 0
			};
			var ts = new TechSpec()
			{
				Colors = new List<ProductColor>() { color },
				SequenceNum = 0,
				TotalQuantity = 0
			};
			return ts;
		}
	}
}
