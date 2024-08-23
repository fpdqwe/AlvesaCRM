using Product = Domain.Entities.Product.ProductModel;

namespace DesktopUI.Utilities.Services
{
    public static class ProductService
    {
        public delegate void ProductsHandler(IList<Product> products);
		public delegate void ProductHandler(Product product);

		public static event ProductsHandler ProductsChangedEvent;
        public static event ProductHandler CurrentChangedEvent;
        public static List<Product> Products { get; private set; } = new List<Product>(20);
        public static Product Current { get; private set; }

        /// <summary>
        /// Changes selected product
        /// </summary>
        /// <param name="product">Product to set</param>
        public static void SetCurrent(Product product)
        {
            if(product != null && Current != product){
				Current = product;
				CurrentChangedEvent.Invoke(product);
			}
        }

        /// <summary>
        /// Changes current dataset of products
        /// </summary>
        /// <param name="products"></param>
        /// <exception cref="NullReferenceException"></exception>
        public static void SetCurrentList(IList<Product> products)
        {
            if(products != null)
            {
                Products = products.ToList();
                ProductsChangedEvent?.Invoke(Products);
            }
            else throw new NullReferenceException();
        }
    }
}
