using Model = DesktopUI.Models.ProductModel;
using Product = Domain.Entities.Product.ProductModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DesktopUI.ViewModels.Products
{
	public class CardVM : INotifyPropertyChanged
	{
		// Fields
        private Model _model;
		private Product _product;

		//Properties
        

		// Ctors
        public CardVM()
        {
			_model = new Model();
			_product = new Product();
			Debug.WriteLine("CardVM initialized");
		}
        public CardVM(Product product)
        {
            _model = new Model();
			_product = product;
        }

        // INotifyPropertyChanged realization
        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
