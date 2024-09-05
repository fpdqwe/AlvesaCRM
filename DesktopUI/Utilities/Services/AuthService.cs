using Domain.Entities;

namespace DesktopUI.Utilities
{
	public static class AuthService
	{
		public delegate void AuthHandler(User user);
		public static event AuthHandler LoginCompleted;
		public static event AuthHandler LogOutCompleted;
		public static event AuthHandler UserChanged;

		public static User? CurrentUser { get; private set; }
		public static RepositoryManager Repository { get; private set; }

		public static void LoginSuccess(User user)
		{
			CurrentUser = user;
			LoginCompleted?.Invoke(user);
		}
		public static void LogOut()
		{
			LogOutCompleted.Invoke(CurrentUser);
			CurrentUser = null;
		}

		public static void ChangeUser(User newUser)
		{
			UserChanged?.Invoke(newUser);
		}
		public static void ChangeCompany(Company company)
		{
			if(CurrentUser == null) { return; }
			CurrentUser.Company = company;
			UserChanged?.Invoke(CurrentUser);
		}

		public static void InitRepositories()
		{
			Repository = new RepositoryManager();
		}
	}
}
