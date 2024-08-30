using DesktopUI.Commands;
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
        private string _title;

        // Properties
        public string Title
        {
            get => _title;
            set
            {
                if(_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public object NavigationVM
        {
            get => _navigationVM;
            set
            {
                _navigationVM = value;
                OnPropertyChanged(nameof(NavigationVM));
            }
        }
		// Ctors
		public EmployeeVM()
        {
            _model = new EmployeeModel();
            AdditionCommand = new RelayCommand(Addition);
            CardCommand = new RelayCommand(Card);
            TableCommand = new RelayCommand(Table);
            NavigationVM = new TableVM();
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
