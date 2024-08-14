using DAL.Interfaces;
using Domain.Entities.Product;

namespace DAL.Repository
{
	public class ProductModelRepository : BaseRepository<ProductModel>
	{
		public ProductModelRepository(IContextManager contextManager) : base(contextManager) { }

		public List<ProductModel> GetLast(int count = 20)
		{
			using (var context = CreateDatabaseContext())
			{
				if(context.ProductModels != null)
				{
					return context.ProductModels
						.OrderByDescending(x => x.Id)
						.Take(count)
						.ToList();
				}
				return new List<ProductModel>();
			}
		}
	}
}
