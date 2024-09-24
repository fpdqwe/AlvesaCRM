using DesktopUI.Commands;
using DesktopUI.Utilities;
using Domain.Entities.Product;
using System.Windows.Input;
using Model = DesktopUI.Models.ProductModel;

namespace DesktopUI.ViewModels.Products
{
	public class SizeVM : BaseViewModel
	{
		public delegate void SizeEvent(SizeVM size);
		public event SizeEvent SizeUpdated;
		public event SizeEvent SizeDeleted;
		// Fields
		private ProductSize _size;
		private Model _model;
		private int _index;
		// Properties
		public int Index
		{
			get => _index;
			set
			{
				if (_index != value)
				{
					_index = value;
					OnPropertyChanged(nameof(Index));
				}
			}
		}
		public ProductSize Size
		{
			get => _size;
			set
			{
				Size = value;
				OnPropertyChanged(nameof(Size));
			}
		}
		// Ctors
        public SizeVM(ProductSize size, Model model, int i)
        {
			_index = i;
			_size = size;
			_model = model;
			InitCommands();
        }

		// Commands
		public ICommand UpdateSizeCommand { get; set; }
		public ICommand DeleteSizeCommand { get; set; }
		// Commands realization
		public async void UpdateSize(object parameter)
		{
			await _model.UpdateSize(Size);
			SizeUpdated?.Invoke(this);
		}
		public async void DeleteSize(object parameter)
		{
			await _model.DeleteSize(Size);
			SizeDeleted?.Invoke(this);
		}
		// Private methods
		private void InitCommands()
		{
			UpdateSizeCommand = new RelayCommand(UpdateSize);
			DeleteSizeCommand = new RelayCommand(DeleteSize);
		}
	}
}
