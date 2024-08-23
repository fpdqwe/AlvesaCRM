using DesktopUI.ViewModels.Products;
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
        public TableItem()
        {
			InitializeComponent();
        }

        public TableItem(Product product)
        {
			InitializeComponent();
			DataContext = new TableItemVM(product);
		}
    }
}
