using DesktopUI.Utilities;
using Model = DesktopUI.Models.ProductModel;
using Color = Domain.Entities.Product.ProductColor;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Products
{
    public class ColorVM : BaseViewModel
    {

        private Color _color;
        private Model _model;
        public Color Color
        {
            get => _color;
            set
            {
                OnPropertyChanged(nameof(Color));
            }
        }

        public ColorVM(Color color)
        {
            _color = color;
            _model = new Model();
        }

		public ICommand UpdateColorCommand { get; set; }
		public ICommand DeleteColorCommand { get; set; }

		public async void UpdateColor(object parameter)
		{
			await _model.UpdateColor(Color);
		}
		public async void DeleteColor(object parameter)
		{
			await _model.DeleteColor(Color);
		}
	}
}
