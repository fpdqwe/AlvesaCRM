using System.Collections.ObjectModel;
using System.ComponentModel;
using Product = Domain.Entities.Product.ProductModel;
using System.Runtime.CompilerServices;
using DesktopUI.Utilities.Services;
using static DesktopUI.Utilities.Services.ProductService;
using Microsoft.EntityFrameworkCore.Query.Internal;
using DesktopUI.Views.CustomControls.Products;
using System.Diagnostics;

namespace DesktopUI.ViewModels.Products
{
	public class TableVM : INotifyPropertyChanged
	{
		public List<TableItem> Items { get; set; }

        public TableVM()
        {
            Init();
			Debug.WriteLine("TableVM initialized");
		}

        public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		private void OnProductsChanged(ProductsHandler products)
		{
			
		}

		private void Init()
		{
			Items = new List<TableItem>(20);
			var source = ProductService.Products;
			foreach (var item in source)
			{
				Items.Add(new TableItem(item));
			}
		}
	}
}
