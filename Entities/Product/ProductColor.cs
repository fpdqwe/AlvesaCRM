using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
    public class ProductColor : IDbEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public int TechSpecId { get; set; }
        public TechSpec TechSpec { get; set; }
        public List<ProductSize> Sizes { get; set; }

		public int GetPrimaryKey()
		{
            return Id;
		}
	}
}
