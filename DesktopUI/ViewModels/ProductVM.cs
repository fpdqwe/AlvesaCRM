using Product = Domain.Entities.Product;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using DesktopUI.Commands;
using DesktopUI.ViewModels.Products;

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
		public object ObserveMode { get; set; }
        public ProductVM()
        {
			AdditionMode = new RelayCommand(OpenAddition, CanOpenAddition);
			CardMode = new RelayCommand(OpenCard, CanOpenCard);
			TableMode = new RelayCommand(OpenTable, CanOpenTable);
        }

		// Navigation commands
		public ICommand AdditionMode { get; set; }
		public ICommand CardMode { get; set; }
		public ICommand TableMode { get; set; }

		private void OpenAddition(object obj)
		{
			ObserveMode = new AdditionVM();
		}
		private void OpenCard(object obj)
		{
			ObserveMode = new CardVM();
		}
		private void OpenTable(object obj)
		{
			ObserveMode = new TableVM();
		}
		private bool CanOpenAddition(object obj)
		{
			return true;
		}
		private bool CanOpenCard(object obj)
		{
			return true;
		}
		private bool CanOpenTable(object obj)
		{
			return true;
		}
	}
}
