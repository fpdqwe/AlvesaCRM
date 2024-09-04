using DAL;
using DAL.Interfaces;
using DAL.Repository;
using DesktopUI.Utilities;
using Domain.Entities;

namespace DesktopUI.Models
{
	class EmployeeModel : IGenericModel<User>
	{
		private UserRepository _userRepository;

        public EmployeeModel()
        {
            _userRepository = new UserRepository(new ContextManager());
        }
        public async Task<bool> Save(User entity)
		{
			if(entity.Id == 0) { entity.Id = _userRepository.GetLastId() + 1; }
			if(await _userRepository.Add(entity) != null)
			{
				return true;
			};
			return false;
		}

		public async Task<bool> Delete(User entity)
		{
			return await _userRepository.Delete(entity);
		}

		public async Task<IList<User>> Read(int count = 20)
		{
			return await _userRepository.GetLastByCompany(AuthService.CurrentUser.Company, count);
		}

		public async Task<bool> Update(User entity)
		{
			if (await _userRepository.Update(entity) != null)
			{
				return true;
			}
			return false;
		}
	}
}
