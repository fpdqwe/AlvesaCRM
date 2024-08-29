using DesktopUI.Utilities;
using Domain.Entities;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Employee
{
    /// <summary>
    /// ViewModel for adding new employee to company
    /// </summary>
	public class AdditionVM : BaseAdditionViewModel<User>
	{

        // Ctors
        public AdditionVM() : base()
        {
            _value = new User();
        }

		// Commands
		protected override void Add(object obj)
		{
			base.Add(obj);
		}


		// Public Methods

	}
}
