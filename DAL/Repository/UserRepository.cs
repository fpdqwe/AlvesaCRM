using DAL.Interfaces;
using Domain.Entities;

namespace DAL.Repository
{
	public class UserRepository : BaseRepository<User>
	{
		public UserRepository(IContextManager contextManager) : base(contextManager) { }

		public User FindByLogin(string login)
		{
            using (var db = CreateDatabaseContext())
            {
                return db.Set<User>().FirstOrDefault(u => u.Login == login);
            }
        }

        public bool ExistUser(string login)
        {
            using (var db = CreateDatabaseContext())
            {
                return db.Set<User>().Count(u => u.Login == login) > 0;
            }
        }

        
    }
}
