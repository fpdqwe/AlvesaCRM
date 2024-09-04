using Product = Domain.Entities.Product.ProductModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using DesktopUI.Commands;
using DesktopUI.ViewModels.Products;
using System.Diagnostics;
using DesktopUI.Utilities.Services;

namespace DesktopUI.ViewModels
{
	/// <summary>
	/// This ViewModel is navigation between CRUD tools for ProductModel entity 
	/// </summary>
	public class ProductVM : INotifyPropertyChanged
	{
		// INotifyPropertyChanged realization
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		// Fields and props
		private object _observeMode;
		public object ObserveMode
		{
			get => _observeMode;
			set
			{
				_observeMode = value;
				OnPropertyChanged(nameof(ObserveMode));
			}
		}

		// Ctor
        public ProductVM()
        {
			AdditionMode = new RelayCommand(OpenAddition);
			CardMode = new RelayCommand(OpenCard);
			TableMode = new RelayCommand(OpenTable);
			ObserveMode = new TableVM();

			ProductService.CurrentChangedEvent += OpenCard;
        }

		// Navigation commands
		public ICommand AdditionMode { get; set; }
		public ICommand CardMode { get; set; }
		public ICommand TableMode { get; set; }

		private void OpenAddition(object obj)
		{
			if(ObserveMode is AdditionVM) return;
			ObserveMode = new AdditionVM();
			Debug.WriteLine($"AdditionVM called from {this}");
		}
		private void OpenCard(object obj)
		{
			if (ObserveMode is CardVM) return;
			ObserveMode = new CardVM();
			Debug.WriteLine($"CardVM called from {this}");
		}
		private void OpenTable(object obj)
		{
			if (ObserveMode is TableVM) return;
			ObserveMode = new TableVM();
			Debug.WriteLine($"TableVM called from {this}");
		}

		// Event handlers
		private void OnCardSelected(Product product)
		{

		}
	}
}
