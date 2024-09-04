using DAL.Interfaces;
using Domain.Entities;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class UserRepository : BaseRepository<User>
	{
		public UserRepository(IContextManager contextManager) : base(contextManager) { }

		public async Task<User> FindByLogin(string login)
		{
            using (var db = CreateDatabaseContext())
            {
                return await db.Set<User>().FirstOrDefaultAsync(u => u.Login == login);
            }
        }

        public async Task<bool> ExistUser(string login)
        {
            using (var db = CreateDatabaseContext())
            {
                return await db.Set<User>().CountAsync(u => u.Login == login) > 0;
            }
        }

		public async Task<List<User>> GetLastByCompany(Company company, int count = 20)
		{
			using (var context = CreateDatabaseContext())
			{
				if (context.Users != null)
				{
					return await context.Users
						.Where(x => x.Company.Id == company.Id)
						.OrderByDescending(x => x.Id)
						.Take(count)
						.ToListAsync();
				}
				return new List<User>();
			}
		}

		public int GetLastId()
		{
			using (var context = CreateDatabaseContext())
			{
				if(context.Users != null)
				{
					var i = context.Users.Count();
					return i;
				}
				return 0;
			}
		}
	}
}
