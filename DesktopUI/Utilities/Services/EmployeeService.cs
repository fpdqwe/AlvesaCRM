using Domain.Entities;

namespace DesktopUI.Utilities.Services
{
    public static class EmployeeService
    {
        public delegate void EmployeeHandler(User user);
        public delegate void ObserverHandler();

        public static event EmployeeHandler EmployeeChanged;
        public static event ObserverHandler MainRequested;

        public static User Current { get; private set; }
        public static void RequestMain()
        {
            MainRequested?.Invoke();
        }
        public static void SetCurrent(User user)
        {
            if(user != null)
            {
                Current = user;
                EmployeeChanged?.Invoke(Current);
            }
        }
    }
}
