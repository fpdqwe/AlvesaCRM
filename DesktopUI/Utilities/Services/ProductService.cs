using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DesktopUI.Utilities.Services
{
    public static class ProductService
    {
        public delegate void ProductsHandler(IList<ProductModel> products);
		public delegate void ProductHandler(ProductModel product);

		public static event ProductsHandler ProductsChanged;
        public static event ProductHandler CurrentChanged;
        public static List<ProductModel> Products { get; private set; } = new List<ProductModel>(20);
        public static ProductModel Current { get; private set; }
        public static void SetCurrent(ProductModel product)
        {
            if(product != null && Current != product){
				Current = product;
				CurrentChanged.Invoke(product);
			}
        }
        /// <summary>
        /// Changes current dataset of products
        /// </summary>
        /// <param name="products"></param>
        /// <exception cref="NullReferenceException"></exception>
        public static void SetCurrentList(IList<ProductModel> products)
        {
            if(products != null)
            {
                Products = products.ToList();
                ProductsChanged.Invoke(Products);
            }
            else throw new NullReferenceException();
        }
    }
}
