using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopUI.Views.CustomControls.Products
{
    /// <summary>
    /// Логика взаимодействия для Addition.xaml
    /// </summary>
    public partial class Addition : UserControl
    {
        public Addition()
        {
            InitializeComponent();
        }

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex(@"^\d*(,\d*)?$");
			e.Handled = !regex.IsMatch((sender as TextBox).Text + e.Text);
		}
    }
}
