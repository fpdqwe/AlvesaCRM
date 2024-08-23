using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesktopUI.ViewModels
{
	class AuthVM : INotifyPropertyChanged
	{
		// Fields
        private AuthModel _authModel;
		private RelayCommand _authCommand;
		private RelayCommand _tryLoginCommand;
		private string _login;
		private string _password;
		private string? _exception;

		// Properties
		public string Exception
		{
			get { return _exception; }
			set
			{
				_exception = value;
				OnPropertyChanged(nameof(Exception));
			}
		}
		public string Login
		{
			get { return _login; }
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		// Ctors
        public AuthVM()
		{
			_authModel = new AuthModel();
			AuthService.LoginCompleted += OnLoginSuccess;
		}

		// Commands
        public RelayCommand AuthCommand
		{
			get
			{
				return _authCommand ??
					(_authCommand = new RelayCommand(async obj =>
					{
						if (await _authModel.TryAuthUser(new User { Login = Login, Password = Password }))
						{
							Exception = "Учётная запись создана!";
						}
						else Exception = "Пользователь уже зарегистрирован";
					}));
			}
		}
		public RelayCommand TryLoginCommand
		{
			get
			{
				return _tryLoginCommand ??
					(_tryLoginCommand = new RelayCommand(async obj =>
					{
						if (await _authModel.TryLogIn(new User { Login = Login, Password = Password}))
						{
							return;
						}
						else Exception = "Неверно введён логин или пароль.";
					}));
			}
		}

		// Public methods
		public void OnLoginSuccess(User user)
		{
			Exception = $"Добро пожаловать!, {user.FirstName}";
			AuthService.LoginCompleted -= OnLoginSuccess;
		}

		// INotifyPropertyChanged realization
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
