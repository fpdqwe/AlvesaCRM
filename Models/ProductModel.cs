using DAL;
using DAL.Repository;

namespace DesktopUI.Models
{
	public class ProductModel
	{
		public ProductModelRepository ProductRepository;

        public ProductModel()
        {
            ProductRepository = new ProductModelRepository(new ContextManager());
        }
    }
}
