using DesktopUI.Utilities;
using Domain.Entities.Product;
using System.Windows.Input;
using Model = DesktopUI.Models.ProductModel;

namespace DesktopUI.ViewModels.Products
{
	public class SizeVM : BaseViewModel
	{
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
        }

		// Commands
		public ICommand UpdateSizeCommand { get; set; }
		public ICommand DeleteSizeCommand { get; set; }
		// Commands realization
		public async void UpdateSize(object parameter)
		{
			await _model.UpdateSize(Size);
		}
		public async void DeleteSize(object parameter)
		{
			await _model.DeleteSize(Size);
		}
		// Private methods
	}
}
