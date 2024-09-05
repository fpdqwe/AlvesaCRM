using DAL;
using DAL.Repository;

namespace DesktopUI.Utilities
{
	public class RepositoryManager
	{
		public ProductModelRepository ProductModelRepository { get;}
		public TechSpecRepository TechSpecRepository { get;}
		public ProductColorRepository ProductColorRepository { get;}
		public ProductSizeRepository ProductSizeRepository { get;}
		public CompanyRepository CompanyRepository { get;}
		public UserRepository UserRepository { get;}

        public RepositoryManager()
        {
            var manager = new ContextManager();
			ProductModelRepository = new ProductModelRepository(manager);
			TechSpecRepository = new TechSpecRepository(manager);
			ProductColorRepository = new ProductColorRepository(manager);
			ProductSizeRepository = new ProductSizeRepository(manager);
			CompanyRepository = new CompanyRepository(manager);
			UserRepository = new UserRepository(manager);
        }
    }
}
