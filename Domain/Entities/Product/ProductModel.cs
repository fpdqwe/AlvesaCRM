using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
    public class ProductModel : IDbEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(25,ErrorMessage = "max modelNumber size is 25 symbols")]
        public string ModelNumber { get; set; }
        public string? Description { get; set; }
        public List<TechSpec> TechSpecs { get; set; } = new List<TechSpec>();
        public int CompanyId { get; set; }
        public Company Company { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
