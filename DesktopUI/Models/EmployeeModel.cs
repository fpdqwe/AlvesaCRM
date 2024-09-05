using DAL;
using DAL.Interfaces;
using DAL.Repository;
using DesktopUI.Utilities;
using Domain.Entities;

namespace DesktopUI.Models
{
	class EmployeeModel : IGenericModel<User>
	{
		private UserRepository _repository;

        public EmployeeModel()
        {
            _repository = AuthService.Repository.UserRepository;
        }
        public async Task<bool> Save(User entity)
		{
			if(entity.Id == 0) { entity.Id = _repository.GetLastId() + 1; }
			if(await _repository.Add(entity) != null)
			{
				return true;
			};
			return false;
		}

		public async Task<bool> Delete(User entity)
		{
			return await _repository.Delete(entity);
		}

		public async Task<IList<User>> Read(int count = 20)
		{
			return await _repository.GetLastByCompany(AuthService.CurrentUser.Company, count);
		}

		public async Task<bool> Update(User entity)
		{
			if (await _repository.Update(entity) != null)
			{
				return true;
			}
			return false;
		}
	}
}
