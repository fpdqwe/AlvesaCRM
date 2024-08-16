using DAL.Interfaces;
using Domain.Entities;
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

        
    }
}
