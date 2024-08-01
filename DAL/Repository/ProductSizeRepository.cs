using DAL.Interfaces;
using Domain.Entities.Product;

namespace DAL.Repository
{
	public class ProductSizeRepository : BaseRepository<ProductSize>
	{
		public ProductSizeRepository(IContextManager contextManager) : base(contextManager)
		{
		}
	}
}
