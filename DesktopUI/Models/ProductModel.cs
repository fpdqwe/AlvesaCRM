using DAL;
using DAL.Repository;
using Product = Domain.Entities.Product.ProductModel;
using DesktopUI.Utilities.Services;
using System.Diagnostics;
using DesktopUI.Utilities;
using Domain.Entities.Product;

namespace DesktopUI.Models
{
	public class ProductModel
	{
		// Fields and properties
		private const string UNASSIGNED = "UNASSIGNED";
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
            ProductRepository = AuthService.Repository.ProductModelRepository;
			_techSpecRepository = AuthService.Repository.TechSpecRepository;
			_colorRepository = AuthService.Repository.ProductColorRepository;
			_sizeRepository = AuthService.Repository.ProductSizeRepository;
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

		public async Task<TechSpec> AddTechSpec(Product product, TechSpec reference = null)
		{
			if (product == null) return null;
			product.TechSpecs.Add(CreateTS(reference));
			var result = await ProductRepository.SaveOrUdate(product);
			return result.TechSpecs.Last();
		}
		public static TechSpec CreateTS(TechSpec reference = null)
		{
			TechSpec result = new TechSpec();
			if (reference != null)
			{
				result.Colors = new List<ProductColor>(reference.Colors.Count);
				foreach (var color in reference.Colors)
				{
					var newColor = new ProductColor();
					newColor.Sizes = new List<ProductSize>(color.Sizes.Count);
					foreach (var size in color.Sizes)
					{
						newColor.Sizes.Add(new ProductSize()
						{
							Name = size.Name,
							Barcode = size.Barcode,
							Quantity = 0
						});
					}
					newColor.TotalQuantity = 0;
					newColor.Name = color.Name;
					result.Colors.Add(newColor);
				}
			}
			else
			{
				var size = new ProductSize()
				{
					Name = UNASSIGNED,
					Barcode = UNASSIGNED,
					Quantity = 0
				};
				var color = new ProductColor()
				{
					Name = UNASSIGNED,
					Sizes = new List<ProductSize>() { size },
					TotalQuantity = 0
				};
				result = new TechSpec()
				{
					Colors = new List<ProductColor>() { color },
					SequenceNum = 0,
					TotalQuantity = 0,
					ProductionPrice = reference.ProductionPrice,
					Price = reference.Price,
				};
			}
			return result;
		}
		public async Task<bool> UpdateTechSpec(TechSpec techSpec)
		{
			if (techSpec == null) { throw new ArgumentNullException(nameof(techSpec)); }
			var result = await _techSpecRepository.Update(techSpec);
			if (result != null) return true;
			return false;
		}
		public async Task<bool> AddColor(ProductColor color)
		{
			if(color == null) throw new ArgumentNullException(nameof(color));
			var result = await _colorRepository.Add(color);
			if(result != null) return true;
			return false;
		}
		public async Task<bool> UpdateColor(ProductColor color)
		{
			if(color == null) throw new ArgumentNullException(nameof(color));
			var result = await _colorRepository.Update(color);
			if(result != null) return true;
			return false;
		}
		public async Task<bool> DeleteColor(ProductColor color)
		{
			if(color == null) throw new ArgumentNullException(nameof(color));
			return await _colorRepository.Delete(color);
		}
		public async Task<bool> AddSize(ProductSize size)
		{
			if(size == null) throw new ArgumentNullException(nameof(size));
			var result = await _sizeRepository.Add(size);
			if(result != null) return true;
			return false;
		}
		public async Task<bool> UpdateSize(ProductSize size)
		{
			if(size == null) throw new ArgumentNullException(nameof(size));
			var result = await _sizeRepository.Update(size);
			if(result != null) return true;
			return false;
		}
		public async Task<bool> DeleteSize(ProductSize size)
		{
			if (size == null) throw new ArgumentNullException(nameof(size));
			return await _sizeRepository.Delete(size);
		}
		public async Task<ProductColor> GenerateNewColor(TechSpec parent, ProductColor reference = null)
		{
			if (reference == null)
			{
				var result = new ProductColor()
				{
					Name = string.Empty,
					TotalQuantity = 0,
					Composition = string.Empty,
					TechSpecId = parent.Id
				};
				result.Sizes = new List<ProductSize> { GenerateNewSize(result) };
				return result;
			}
			else
			{
				var result = new ProductColor()
				{
					Name = reference.Name,
					TotalQuantity = reference.TotalQuantity,
					Composition = reference.Composition,
					TechSpecId = parent.Id
				};
				var savedColor = await _colorRepository.Add(result);
				savedColor.Sizes = new List<ProductSize>(reference.Sizes.Count);
				foreach(var size in reference.Sizes)
				{
					var savedSize = await _sizeRepository.Add(new ProductSize()
					{
						Name = size.Name,
						Id = 0,
						Barcode = size.Barcode,
						ColorId = savedColor.Id,
						Quantity = size.Quantity,
					});
					savedColor.Sizes.Add(savedSize);
				}
				return savedColor;
			}
		}
		public ProductSize GenerateNewSize(ProductColor parent)
		{
			var result = new ProductSize()
			{
				ColorId = parent.Id,
				Name = string.Empty,
				Barcode = string.Empty,
				Quantity = 0,
			};
			return result;
		}
	}
}
