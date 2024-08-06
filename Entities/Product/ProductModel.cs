using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
    public class ProductModel : IDbEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public decimal? Price { get; set; }
        public decimal? ProductionPrice { get; set; }
        public List<TechSpec> TechSpecs { get; set; } = new List<TechSpec>();

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
