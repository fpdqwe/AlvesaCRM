using Product = Domain.Entities.Product;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using DesktopUI.Commands;
using DesktopUI.ViewModels.Products;
using System.Diagnostics;

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
			ObserveMode = new AdditionVM();
        }

		// Navigation commands
		public ICommand AdditionMode { get; set; }
		public ICommand CardMode { get; set; }
		public ICommand TableMode { get; set; }

		private void OpenAddition(object obj)
		{
			ObserveMode = new AdditionVM();
			Debug.WriteLine($"AdditionVM called from {this}");
		}
		private void OpenCard(object obj)
		{
			ObserveMode = new CardVM();
			Debug.WriteLine($"CardVM called from {this}");
		}
		private void OpenTable(object obj)
		{
			ObserveMode = new TableVM();
			Debug.WriteLine($"TableVM called from {this}");
		}
	}
}
