using DAL;
using DAL.Repository;
using Domain.Entities;

namespace DesktopUI.Models
{
    public class AuthModel
    {
        public UserRepository UserRepository;

        public AuthModel()
        {
            UserRepository = new UserRepository(new ContextManager(/*"Host=localhost;Username=postgres;Password=admin;Database=Alvesa"*/));
        }

  //      public string TryAuth(User credentials)
  //      {
		//	var user = UserRepository.FindByLogin(credentials.Login);
		//	if (user == null)
		//	{
		//		user = new User();
		//		user.Login = credentials.Login;
		//		user.Password = credentials.Password;
		//		UserRepository.Add(user);
  //              AuthCompleted(user);
  //              return "Учётная запись создана!";
		//	}
		//	else return "Пользователь уже зарегистрирован";
		//}

  //      public string TryLogin(User credentials)
  //      {
		//	var user = UserRepository.FindByLogin(credentials.Login);
		//	if (user != null && user.Password == credentials.Password)
		//	{
		//		AuthCompleted(user);
		//		return $"Добро пожаловать!, {credentials.Login}";
		//	}
		//	else return "Неверно введён логин или пароль.";
		//}
    }
}
