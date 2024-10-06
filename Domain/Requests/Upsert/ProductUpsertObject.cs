using System.ComponentModel.DataAnnotations;

namespace Domain.Requests.Upsert
{
	public class ProductUpsertObject
	{
		[Required]
		public required string Name { get; set; }

		[Required]
		public required string Description { get; set; }

		[Required]
		[Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
		public decimal Price { get; set; }
	}
}