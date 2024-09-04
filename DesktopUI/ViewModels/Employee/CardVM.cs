using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using DesktopUI.Utilities.Services;
using Domain.Entities;
using Domain.Enums;
using System.Windows.Input;

namespace DesktopUI.ViewModels.Employee
{
	public class CardVM : BaseViewModel
	{
		// Fields
		private string[] _userTypes = { "Раскройщик", "Мастер", "Швея", "Утюжильщик", "Упаковщик", "Швея" };
		private User _user;
		private EmployeeModel _model;
		private bool _isNotificationOpen;

		// Properties
		public User User
		{
			get => _user;
			set
			{
				_user = value;
				OnPropertyChanged(nameof(User));
			}
		}
		public string UserStringType
		{
			get
			{
				switch (_user.UserType)
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
						_user.UserType = UserType.Cutter;
						OnPropertyChanged(nameof(UserStringType));
						break;
					case "Мастер":
						_user.UserType = UserType.Foreman;
						OnPropertyChanged(nameof(UserStringType));
						break;
					case "Швея":
						_user.UserType = UserType.Seamstress;
						OnPropertyChanged(nameof(UserStringType));
						break;
					case "Утюжильщик":
						_user.UserType = UserType.Ironer;
						OnPropertyChanged(nameof(UserStringType));
						break;
					case "Упаковщик":
						_user.UserType = UserType.Packer;
						OnPropertyChanged(nameof(UserStringType));
						break;
					default:
						_user.UserType = UserType.Seamstress;
						OnPropertyChanged(nameof(UserStringType));
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
		public bool IsNotificationOpen
		{
			get => _isNotificationOpen;
			set
			{
				_isNotificationOpen = value;
				OnPropertyChanged(nameof(IsNotificationOpen));
			}
		}
		// Ctors
		public CardVM()
        {
			_isNotificationOpen = false;
            _model = new EmployeeModel();
			_user = EmployeeService.Current;
			UpdateCommand = new RelayCommand(Update);
        }

		// Commands
		public ICommand UpdateCommand { get; set; }

		// Public mehods
		public void PopupFadeCompleted()
		{
			IsNotificationOpen = false;
		}

		// Private methods
		private async void Update(object obj)
		{
			if(User != null)
			{
				if(await _model.Update(User))
				{
					IsNotificationOpen = true;
				}
			}
		}
    }
}
