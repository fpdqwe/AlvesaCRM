using DesktopUI.Utilities;
using Model = DesktopUI.Models.ProductModel;
using Color = Domain.Entities.Product.ProductColor;
using Size = Domain.Entities.Product.ProductSize;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Products
{
    public class ColorVM : BaseViewModel
    {
        // Fields
        private Color _color;
        private ObservableCollection<SizeVM> _sizes;
        private SizeVM _selectedSize;
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
		public Color Color
        {
            get => _color;
            set
            {
                OnPropertyChanged(nameof(Color));
            }
        }
        public ObservableCollection<SizeVM> Sizes
        {
            get => _sizes;
            set
            {
                OnPropertyChanged(nameof(Sizes));
            }
        }
        public SizeVM SelectedSize
        {
            get => _selectedSize;
            set
            {
                _selectedSize = value;
                OnPropertyChanged(nameof(SelectedSize));
            }
        }
        // Ctors
        public ColorVM(Color color)
        {
            _color = color;
            _sizes = GetSizeCollection(color.Sizes);
            _model = new Model();
        }

        // Commands
		public ICommand UpdateColorCommand { get; set; }
		public ICommand DeleteColorCommand { get; set; }

        // Commands realization
		private async void UpdateColor(object parameter)
		{
			await _model.UpdateColor(Color);
		}
		private async void DeleteColor(object parameter)
		{
			await _model.DeleteColor(Color);
		}

		// Private methods and utilities
		private ObservableCollection<SizeVM> GetSizeCollection(List<Size> sizes)
		{
			var result = new ObservableCollection<SizeVM>();
            int i = 1;
			foreach (var size in sizes)
			{
				result.Add(new SizeVM(size, _model, i));
                i++;
			}
			return result;
		}
	}
}
