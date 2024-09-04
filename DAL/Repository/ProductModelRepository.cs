using DAL.Interfaces;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class ProductModelRepository : BaseRepository<ProductModel>
	{
		public ProductModelRepository(IContextManager contextManager) : base(contextManager) { }

		public async Task<List<ProductModel>> GetLast(int count = 20)
		{
			using (var context = CreateDatabaseContext())
			{
				if(context.ProductModels != null)
				{
					var list = await context.ProductModels
						.OrderByDescending(x => x.Id)
						.Take(count)
						.ToListAsync();
					return list;
				}
				return new List<ProductModel>();
			}
		}
	}
}
