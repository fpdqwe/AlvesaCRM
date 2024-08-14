using DAL;
using DAL.Repository;
using DesktopUI.Utilities;
using Domain.Entities;

namespace DesktopUI.Models
{
    public class AuthModel
    {
        private UserRepository _userRepository;
		private CompanyRepository _companyRepository;

        public AuthModel()
        {
			var contextManager = new ContextManager();
            _userRepository = new UserRepository(contextManager);
			_companyRepository = new CompanyRepository(contextManager);
        }

		public bool TryAuthUser(User credentials)
		{
			var user = _userRepository.FindByLogin(credentials.Login);
			if (user == null)
			{
				credentials.Company = CreateNewCompany(credentials);
				//_companyRepository.Add(credentials.Company);
				
				_userRepository.Add(credentials);
				AuthService.LoginSuccess(credentials);
				return true;
			}
			else return false;
		}
		
		public bool TryLogIn(User credentials)
		{
			var existingUser = _userRepository.FindByLogin(credentials.Login);
			if(existingUser != null && existingUser.Password == credentials.Password)
			{
				AuthService.LoginSuccess(existingUser);
				return true;
			}
			else { return false; }
		}

		private static Company CreateNewCompany(User credentials)
		{
			var company = new Company();
			company.Name = "Название компании ещё не задано";
			company.Type = Domain.Enums.CompanyType.Individual;
			company.Employee = new List<User> { credentials };
			return company;
		}
    }
}
