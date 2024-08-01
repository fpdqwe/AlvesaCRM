using DAL.Interfaces;
using Domain.Entities.Product;

namespace DAL.Repository
{
	public class ProductColorRepository : BaseRepository<ProductColor>
	{
		public ProductColorRepository(IContextManager contextManager) : base(contextManager)
		{
		}
	}
}
