using DesktopUI.Utilities.Services;
using System.Diagnostics;
using System.Windows.Controls;
using Product = Domain.Entities.Product.ProductModel;

namespace DesktopUI.Views.CustomControls.Products
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
            Debug.WriteLine("Table view initialized");
            ProductService.ProductsChangedEvent += OnProductsChanged;
        }

        private void OnProductsChanged(IList<Product> products)
        {
            CardsContainer.Children.Clear();
            foreach (var item in products)
            {
                Border border = new Border();
                var tableItem = new TableItem(item);
                border.Margin = new System.Windows.Thickness(0, 10, 0, 10);
                border.Child = tableItem;
                CardsContainer.Children.Add(border);
            }
        }
    }
}
