namespace Domain.DTOs
{
	public class PagedResult<T>
	{
		public IEnumerable<T> Items { get; set; } = [];
		public int Count { get; set; }
	}
}
