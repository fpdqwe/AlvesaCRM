using DAL.Interfaces;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class ProductSizeRepository : BaseRepository<ProductSize>
	{
		public ProductSizeRepository(IContextManager contextManager) : base(contextManager)
		{
		}

		public async Task<IList<ProductSize>> FindByColor(int colorId)
		{
			using (var db = CreateDatabaseContext())
			{
				return await db.Set<ProductSize>()
					.Where(x => x.ColorId == colorId)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			}
		}
	}
}
