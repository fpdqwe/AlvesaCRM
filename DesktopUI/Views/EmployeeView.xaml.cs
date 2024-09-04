using DesktopUI.Utilities.Services;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopUI.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        public EmployeeView()
        {
            InitializeComponent();
        }

		private void Grid_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.Key == Key.Escape) { EmployeeService.RequestMain(); }
        }
    }
}
