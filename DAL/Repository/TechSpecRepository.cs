using DAL.Interfaces;
using Domain.Entities;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class TechSpecRepository : BaseRepository<TechSpec>
	{
		public TechSpecRepository(IContextManager contextManager) : base(contextManager)
		{
		}

		public async Task<IList<TechSpec>> FindByModel(int modelId)
		{
			using (var db = CreateDatabaseContext())
			{
				var result = await db.Set<TechSpec>()
					.Where(x => x.ModelId == modelId)
					.OrderByDescending(x => x.SequenceNum)
					.ToListAsync();

				foreach (var item in result)
				{
					item.Colors = await db.Set<ProductColor>()
						.Where(x => x.TechSpecId == item.Id)
						.OrderByDescending(x => x.Id)
						.ToListAsync();
					foreach (var color in item.Colors)
					{
						color.Sizes = await db.Set<ProductSize>()
							.Where(x => x.ColorId == color.Id)
							.OrderByDescending(x => x.Id)
							.ToListAsync();
					}
				}

				return result;
			}
		}
	}
}
