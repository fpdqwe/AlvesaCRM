using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
	public class NavigationVM : INotifyPropertyChanged
	{
		//Fields
		private NavigationModel _navigationModel;
		private User _currentUser;
		private object _currentVM;
		private int _navBarWidth;
		private bool _isNavBarVisible;
		//Props
		public User CurrentUser
		{
			get => _currentUser;
			set
			{
				if (value == null) return;
				_currentUser = value;
				OnPropertyChanged(nameof(CurrentUser));
				IsNavBarVisible = true;
			}
		}
		public object CurrentVM
		{
			get { return _currentVM; }
			set
			{
				_currentVM = value;
				OnPropertyChanged(nameof(CurrentVM));
			}
		}
		public bool IsNavBarVisible
		{
			get => _isNavBarVisible;
			set
			{
				_isNavBarVisible = value;
				OnPropertyChanged(nameof(IsNavBarVisible));
			}
		}
		public int NavBarWidth
		{
			get => _navBarWidth;
			set
			{
				_navBarWidth = value;
				OnPropertyChanged(nameof(NavBarWidth));
			}
		}
		//Ctors
        public NavigationVM()
        {
			AuthService.LoginCompleted += OnAuthCompleted;
			OpenAuthCommand = new RelayCommand(AuthView);
			OpenMainCommand = new RelayCommand(MainView);
			OpenProductsCommand = new RelayCommand(ProductView);
			AuthView(null);
        }
		//Commands
		public ICommand OpenAuthCommand { get; set; }
		public ICommand OpenMainCommand { get; set; }
		public ICommand OpenProductsCommand { get; set; }
		//Methods
		private void AuthView(object obj)
		{
			var auth = new AuthVM();
			CurrentVM = auth;
			IsNavBarVisible = false;
			NavBarWidth = 0;
		}
		private void MainView(object obj)
		{
			CurrentVM = new MainPageVM();
		}
		private void ProductView(object obj)
		{
			CurrentVM = new ProductVM();
		}
		public void OnAuthCompleted(User user)
		{
			_navigationModel = new NavigationModel(user);
			Debug.WriteLine("=============");
			Debug.WriteLine($"Current user: {user.FirstName}, id {user.Id}");
			NavBarWidth = 228;
			IsNavBarVisible = true;
			CurrentUser = user;

			OpenMainCommand.Execute(user);
		}


        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
