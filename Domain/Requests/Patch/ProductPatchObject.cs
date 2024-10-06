using System.ComponentModel.DataAnnotations;

namespace Domain.Requests.Patch
{
	public class ProductPatchObject
	{
		[Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
		public decimal Price { get; set; }
	}
}