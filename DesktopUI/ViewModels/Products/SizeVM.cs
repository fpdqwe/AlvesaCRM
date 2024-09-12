using DesktopUI.Utilities;
using Domain.Entities.Product;

namespace DesktopUI.ViewModels.Products
{
	public class SizeVM : BaseViewModel
	{
		private ProductSize _size;
		private ProductModel _model;
		public ProductSize Size
		{
			get => _size;
			set
			{
				Size = value;
				OnPropertyChanged(nameof(Size));
			}
		}

        public SizeVM(ProductSize size)
        {
			_size = size;
        }


    }
}
