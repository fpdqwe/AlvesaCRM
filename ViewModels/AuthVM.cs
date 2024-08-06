using DAL.Repository;
using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;

namespace DesktopUI.ViewModels
{
	class AuthVM : INotifyPropertyChanged
	{
        private AuthModel _authModel;
		private RelayCommand _authCommand;
		private RelayCommand _tryLoginCommand;
		private string _login;
		private string _password;
		private string? _exception;

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

        public AuthVM() { _authModel = new AuthModel(); }
        public RelayCommand AuthCommand
		{
			get
			{
				return _authCommand ??
					(_authCommand = new RelayCommand(obj =>
					{
						var user = _authModel.UserRepository.FindByLogin(Login);
						if (user == null)
						{
							user = new User();
							user.Login = Login;
							user.Password = Password;
							_authModel.UserRepository.Add(user);
							AuthService.LoginSuccess(user);
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
					(_tryLoginCommand = new RelayCommand(obj =>
					{
						var user = _authModel.UserRepository.FindByLogin(Login);
						if (user != null && user.Password == Password)
						{
							Exception = $"Добро пожаловать!, {user.FirstName}";
							AuthService.LoginSuccess(user);
						}
						else Exception = "Неверно введён логин или пароль.";
					}));
			}
		}


		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
