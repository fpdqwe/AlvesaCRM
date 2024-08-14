using DAL;
using DAL.Repository;

namespace DesktopUI.Models
{
	public class ProductModel
	{
		public ProductModelRepository ProductRepository;
        private TechSpecRepository _techSpecRepository;
        private ProductColorRepository _colorRepository;
        private ProductSizeRepository _sizeRepository;

        public ProductModel()
        {
            var contextManager = new ContextManager();
            ProductRepository = new ProductModelRepository(contextManager);
            _techSpecRepository = new TechSpecRepository(contextManager);
            _colorRepository = new ProductColorRepository(contextManager);
            _sizeRepository = new ProductSizeRepository(contextManager);
        }
    }
}
