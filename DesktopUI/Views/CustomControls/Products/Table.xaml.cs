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
        }
    }
}
