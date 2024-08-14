using Domain.Entities.Product;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Company : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required(AllowEmptyStrings = true, ErrorMessage = @"Please input company name")]
		[StringLength(200, ErrorMessage = @"CompanyName должен быть меньше 200 символов")]
		public string? Name { get; set; }
		public CompanyType Type { get; set; }
		public string? INN { get; set; }
		public string? KPP { get; set; }
		public string? Adress { get; set; }
		public string? SettlementAccount { get; set; }
		public List<ProductModel> Products { get; set; }
		public List<User> Employee { get; set; }
		public List<IndivisibleOperation> IndivisibleOperations { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
