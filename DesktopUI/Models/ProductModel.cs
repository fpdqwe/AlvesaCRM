using DAL;
using DAL.Repository;
using Product = Domain.Entities.Product.ProductModel;
using DesktopUI.Utilities.Services;
using System.Diagnostics;

namespace DesktopUI.Models
{
	public class ProductModel
	{
        // Fields and properties
		public ProductModelRepository ProductRepository;
        private TechSpecRepository _techSpecRepository;
        private ProductColorRepository _colorRepository;
        private ProductSizeRepository _sizeRepository;

        // Delegates and events
        public delegate void PruductListHandler(List<Product> product);
        public event PruductListHandler ProductsUpdated;

        // Ctors
        public ProductModel()
        {
            var contextManager = new ContextManager();
            ProductRepository = new ProductModelRepository(contextManager);
            _techSpecRepository = new TechSpecRepository(contextManager);
            _colorRepository = new ProductColorRepository(contextManager);
            _sizeRepository = new ProductSizeRepository(contextManager);
        }

		// Public methods

		public async Task<bool> Add(Product entity)
		{
			try
			{
				await ProductRepository.SaveOrUdate(entity);
			}
			catch (Exception ex) { return false; }
			ProductService.SetCurrentList(await GetLast());
            return true;
		}

		public async Task<IList<Product>> GetLast(int count = 20)
		{
			var newList = await ProductRepository.GetLast(count);
			foreach (var item in newList)
			{
				var temp = await _techSpecRepository.FindByModel(item.Id);
				item.TechSpecs = temp.ToList();
			}
			Debug.WriteLine(newList.First().TechSpecs);
			ProductService.SetCurrentList(newList);
			return newList;
		}

		// Private mehods



	}
}
