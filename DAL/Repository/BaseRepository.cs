using DAL.Interfaces;
using Domain.Entities;

namespace DAL.Repository
{
	public abstract class BaseRepository<T> : IRepository<T> where T : class
	{
		private readonly IContextManager _contextManager;
        protected BaseRepository(IContextManager contextManager)
        {
            _contextManager = contextManager;
        }
		public ApplicationDbContext CreateDatabaseContext()
		{
			return _contextManager.CreateDatabaseContext();
		}
        public T Add(T entity)
        {
            using (var context = CreateDatabaseContext())
            {
                var iDbEntity = entity as IDbEntity;

                if (iDbEntity == null)
                    throw new ArgumentException("entity should be IDbEntity type", "entity");

                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
            return entity;
        }
        public void Delete(T entity)
		{
            using (var context = CreateDatabaseContext())
            {
                var iDbEntity = entity as IDbEntity;

                if (iDbEntity == null)
                    throw new ArgumentException("entity should be IDbEntity type", "entity");

                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

		public T Find(int entityId)
		{
			using(var context = CreateDatabaseContext())
			{
                return context.Set<T>().Find(entityId);
            }
		}

		public List<T> GetAll()
		{
			using(var context = CreateDatabaseContext())
			{
				return context.Set<T>().ToList();
			}
		}

		public T SaveOrUdate(T entity)
		{
            var iDbEntity = entity as IDbEntity;

            if (iDbEntity == null)
                throw new ArgumentException("entity should be IDbEntity type", "entity");

            return iDbEntity.GetPrimaryKey() == 0 ? Add(entity) : Update(entity);

        }

		public T Update(T entity)
		{
            using (var context = CreateDatabaseContext())
            {
                var iDbEntity = entity as IDbEntity;
                if (iDbEntity == null)
                    throw new ArgumentException("entity should be IDbEntity type", "entity");

                var attachedEntity = context.Set<T>().Find(iDbEntity.GetPrimaryKey());
                context.Set<T>().Update(attachedEntity);
                context.SaveChanges();
            }
            return entity;
        }
	}
}
