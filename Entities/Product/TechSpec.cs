using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
    public class TechSpec : IDbEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SequenceNum { get; set; }
        public int TotalQuantity { get; set; }
        public int ModelId { get; set; }
        public ProductModel ProductModel { get; set; }
        public List<ProductColor> Colors { get; set; } = new List<ProductColor>();

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
