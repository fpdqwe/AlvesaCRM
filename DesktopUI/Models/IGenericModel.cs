using Domain.Entities;

namespace DesktopUI.Models
{
	public interface IGenericModel<T> where T : IDbEntity
	{
		public Task<IList<T>> Read(int count = 20); 
		public Task<bool> Create(T entity);
		public Task<bool> Update(T entity);
		public Task<bool> Delete(T entity);
	}
}
