using DAL.Interfaces;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class ProductColorRepository : BaseRepository<ProductColor>
	{
		public ProductColorRepository(IContextManager contextManager) : base(contextManager)
		{
		}

		public async Task<IList<ProductColor>> FindByTechSpec(int techSpecId)
		{
			using (var db = CreateDatabaseContext())
			{
				return await db.Set<ProductColor>()
					.Where(x => x.TechSpecId == techSpecId)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			}
		}
	}
}
