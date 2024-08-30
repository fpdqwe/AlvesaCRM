using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Employee
{
    /// <summary>
    /// ViewModel for adding new employee to company
    /// </summary>
	public class AdditionVM : BaseViewModel
	{
		// Fields
		private User _newUser;
		private EmployeeModel _model;

		// Properties
		public User NewUser
		{
			get => _newUser;
			set
			{
				_newUser = value;
				OnPropertyChanged(nameof(NewUser));
			}
		}

        // Ctors
        public AdditionVM()
        {
            _model = new EmployeeModel();
			_newUser = new User();
        }

		// Commands
		public ICommand AddCommand { get; set; }
		public async void Add(object obj)
		{
			if(await _model.Create(NewUser))
			{

			}
		}

		// Public Methods
		public void PopupFadeCompleted()
		{

		}


	}
}
