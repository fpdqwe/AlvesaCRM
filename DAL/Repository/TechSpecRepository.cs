using DAL.Interfaces;
using Domain.Entities.Product;

namespace DAL.Repository
{
	public class TechSpecRepository : BaseRepository<TechSpec>
	{
		public TechSpecRepository(IContextManager contextManager) : base(contextManager)
		{
		}
	}
}
