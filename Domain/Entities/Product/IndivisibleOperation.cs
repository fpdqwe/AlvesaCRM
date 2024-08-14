using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
	public class IndivisibleOperation : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal Time { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public DateTime? DateModified { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public List<UnitedOperation> Parents { get; set; }
		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
