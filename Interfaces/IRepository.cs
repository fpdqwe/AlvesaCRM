namespace DAL.Interfaces
{
	public interface IRepository<T> where T : class
	{
		ApplicationDbContext CreateDatabaseContext();
		List<T> GetAll();
		T Find(int entityId);
		T SaveOrUdate(T entity);
		T Add(T entity);
		T Update(T entity);
		void Delete(T entity);
	}
}
