using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DesktopUI.Views.CustomControls.Employee
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

		private async void Popup_Opened(object sender, EventArgs e)
		{
			var popup = sender as Popup;
			await Task.Delay(3000);
			popup.IsOpen = false;
        }
    }
}
