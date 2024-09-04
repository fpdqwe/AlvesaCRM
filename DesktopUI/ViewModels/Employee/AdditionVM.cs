using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using Domain.Enums;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Employee
{
    /// <summary>
    /// ViewModel for adding new employee to company
    /// </summary>
	public class AdditionVM : BaseViewModel
	{
		// Fields
		private string[] _userTypes = { "Раскройщик", "Мастер", "Швея", "Утюжильщик", "Упаковщик", "Швея" };
		private User _newUser;
		private EmployeeModel _model;
		private bool _isNotificationOpen;

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
		public bool IsNotificationOpen 
		{
			get => _isNotificationOpen;
			set
			{
				_isNotificationOpen = value;
				OnPropertyChanged(nameof(IsNotificationOpen));
			}
		}
		public string NewUserType
		{
			get
			{
				switch (_newUser.UserType)
				{
					case UserType.Cutter:
						return "Раскройщик";
					case UserType.Foreman:
						return "Мастер";
					case UserType.Seamstress:
						return "Швея";
					case UserType.Ironer:
						return "Утюжильщик";
					case UserType.Packer:
						return "Упаковщик";
					default:
						return "Швея";
				}
			}
			set
			{
				switch (value)
				{
					case "Раскройщик":
						_newUser.UserType = UserType.Cutter;
						OnPropertyChanged(nameof(NewUserType));
						break;
					case "Мастер":
						_newUser.UserType = UserType.Foreman;
						OnPropertyChanged(nameof(NewUserType));
						break;
					case "Швея":
						_newUser.UserType = UserType.Seamstress;
						OnPropertyChanged(nameof(NewUserType));
						break;
					case "Утюжильщик":
						_newUser.UserType = UserType.Ironer;
						OnPropertyChanged(nameof(NewUserType));
						break;
					case "Упаковщик":
						_newUser.UserType = UserType.Packer;
						OnPropertyChanged(nameof(NewUserType));
						break;
					default:
						_newUser.UserType = UserType.Seamstress;
						OnPropertyChanged(nameof(NewUserType));
						break;
				}
			}
		}
		public string[] UserTypes
		{
			get => _userTypes;
			set
			{
				_userTypes = value;
				OnPropertyChanged(nameof(UserTypes));
			}
		}

        // Ctors
        public AdditionVM()
        {
			_isNotificationOpen = false;
            _model = new EmployeeModel();
			_newUser = new User();
			_newUser.CompanyId = AuthService.CurrentUser.Company.Id;
			_newUser.UserType = UserType.Seamstress;
			AddCommand = new RelayCommand(Add);
        }

		// Commands
		public ICommand AddCommand { get; set; }
		public async void Add(object obj)
		{
			if(await _model.Save(NewUser))
			{
				IsNotificationOpen = true;
			}
		}

		// Public Methods
		public void PopupFadeCompleted()
		{
			IsNotificationOpen = false;
		}
	}
}
