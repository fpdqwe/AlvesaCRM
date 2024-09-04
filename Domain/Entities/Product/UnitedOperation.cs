using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Product
{
	public class UnitedOperation : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public ProductModel Product { get; set; }
		public int ProductId { get; set; }
		public List<IndivisibleOperation> Operations { get; set; }
		public int[] OperationsIds { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal TotalTime { get; set; }
		public int RouteId { get; set; }
		public int RouteVersion { get; set; }
		public DateTime RouteVersionStart { get; set; }
		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
