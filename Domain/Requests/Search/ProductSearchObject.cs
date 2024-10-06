namespace Domain.Requests.Search
{
	public class ProductSearchObject : BaseSearchObject
	{
		public string? FullTextSearch { get; set; }
	}
}