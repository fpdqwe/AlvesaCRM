using DesktopUI.Models;
using DesktopUI.Utilities;
using DesktopUI.ViewModels.Employee;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
	/// <summary>
	/// This ViewModel is navigation between CRUD tools for User entity 
	/// </summary>
	public class EmployeeVM : BaseViewModel
	{
        // Fields
		private EmployeeModel _model;
        private object _navigationVM;

        // Properties
        public object NavigationVM
        {
            get => _navigationVM;
            set
            {
                _navigationVM = value;
                OnPropertyChanged(nameof(NavigationVM));
            }
        }
        public EmployeeVM()
        {
            _model = new EmployeeModel();
        }

        // Commands
        public ICommand AdditionCommand { get; set; }
        public ICommand CardCommand { get; set; }
        public ICommand TableCommand { get; set; }
        private void Addition(object obj)
        {
            NavigationVM = new AdditionVM();
        }
        private void Card(object obj)
        {
            NavigationVM = new CardVM();
        }
        private void Table(object obj)
        {
            NavigationVM = new TableVM();
        }

        // Public Methods

        // Private Methods
    }
}
