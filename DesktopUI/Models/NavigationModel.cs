using Domain.Entities;

namespace DesktopUI.Models
{
	internal class NavigationModel
	{
		public User CurrentUser { get; private set; }

        public NavigationModel(User user)
        {
            CurrentUser = user;
        }
	}
}
