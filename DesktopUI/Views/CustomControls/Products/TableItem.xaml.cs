using System.Windows.Controls;
using System.Windows.Input;
using Product = Domain.Entities.Product.ProductModel;

namespace DesktopUI.Views.CustomControls.Products
{
    /// <summary>
    /// Main table of products
    /// </summary>
    public partial class TableItem : UserControl
    {
        private Product _product { get; set; }

        public string Title { get => _product.Name; set => _product.Name = value; }
        public string Price { get => _product.Price.ToString(); }
        public TableItem()
        {
			InitializeComponent();
        }

        public TableItem(Product product)
        {
            _product = product;
			InitializeComponent();
		}
    }
}
