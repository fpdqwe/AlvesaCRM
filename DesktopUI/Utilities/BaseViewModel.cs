using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesktopUI.Utilities
{
	/// <summary>
	/// Includes basic INotifyPropertyChanged realization
	/// </summary>
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public virtual void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
