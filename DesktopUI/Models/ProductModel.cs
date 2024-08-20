using DAL;
using DAL.Repository;
using Entities = Domain.Entities.Product;
using DesktopUI.Utilities.Services;
using System.Collections;
using System.Diagnostics;

namespace DesktopUI.Models
{
	public class ProductModel
	{
		public ProductModelRepository ProductRepository;
        private TechSpecRepository _techSpecRepository;
        private ProductColorRepository _colorRepository;
        private ProductSizeRepository _sizeRepository;

        public delegate void PruductListHandler(List<Entities.ProductModel> product);
        public event PruductListHandler ProductsUpdated;

        public ProductModel()
        {
            var contextManager = new ContextManager();
            ProductRepository = new ProductModelRepository(contextManager);
            _techSpecRepository = new TechSpecRepository(contextManager);
            _colorRepository = new ProductColorRepository(contextManager);
            _sizeRepository = new ProductSizeRepository(contextManager);
        }

        //public async void GetLast(int count = 20)
        //{
        //    var newList = await ProductRepository.GetLast(count);
        //    ProductsUpdated?.Invoke(newList);
        //}

        private async IList<Entities.ProductModel> GetLast(int count = 20)
        {
            var newList = await ProductRepository.GetLast(count);
            Debug.WriteLine(newList.First().TechSpecs);
            ProductService.SetCurrentList(newList);
        }

        
    }
}
