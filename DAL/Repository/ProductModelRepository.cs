using DAL.Interfaces;
using Domain.Entities.Product;

namespace DAL.Repository
{
	public class ProductModelRepository : BaseRepository<ProductModel>
	{
		public ProductModelRepository(IContextManager contextManager) : base(contextManager) { }
	}
}
