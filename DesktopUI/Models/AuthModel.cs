using DesktopUI.Models.Exceptions;
using DAL.Repository;
using DesktopUI.Utilities;
using Domain.Entities;

namespace DesktopUI.Models
{
    public class AuthModel
    {
		private static bool _isRequesting = false;
        private UserRepository _userRepository;
		private CompanyRepository _companyRepository;

        public AuthModel()
        {
			_userRepository = AuthService.Repository.UserRepository;
			_companyRepository = AuthService.Repository.CompanyRepository;
        }

		public async Task<bool> TryAuthUser(User credentials)
		{
			if (_isRequesting) { throw new MultipleRequestsException(); }
			_isRequesting = true;
			var user = await _userRepository.FindByLogin(credentials.Login);
			if (user == null)
			{
				credentials.Company = CreateNewCompany(credentials);
				//_companyRepository.Add(credentials.Company);
				
				await _userRepository.Add(credentials);
				AuthService.LoginSuccess(credentials);
				_isRequesting = false;
				return true;
			}
			else
			{
				_isRequesting = false;
				return false;
			}
		}
		
		public async Task<bool> TryLogIn(User credentials)
		{
			var existingUser = await _userRepository.FindByLogin(credentials.Login);
			if(existingUser != null && existingUser.Password == credentials.Password)
			{
				if(existingUser.Company == null)
				{
					existingUser.Company = await _companyRepository.Find(existingUser.Id);
				}
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
