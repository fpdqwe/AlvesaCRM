using Domain.Entities;

namespace DesktopUI.Utilities
{
	public static class AuthService
	{
		public delegate void LoginHandler(User user);
		public static event LoginHandler LoginCompleted;

		public static void LoginSuccess(User user)
		{
			LoginCompleted?.Invoke(user);
		}
	}
}
