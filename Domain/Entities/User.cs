using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class User : IDbEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; } = 0;
		[Required(AllowEmptyStrings = false, ErrorMessage = @"Please input password")]
		[StringLength(50, ErrorMessage = @"Password должен быть меньше 50 символов")]
		public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Please input login")]
        [StringLength(50, ErrorMessage = @"Login должен быть меньше 50 символов")]
        public string Login { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public UserType UserType { get; set; }
		public int CompanyId { get; set; }
		public Company Company { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;


		public override string ToString()
		{
			return $"Тип: User, Логин: {Login}, Пароль: {Password}";
		}
		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
