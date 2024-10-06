using Domain.DTOs;
using Domain.Requests.Search;

namespace Domain.Interfaces
{
	public interface IReadRepository<T, TSearch> where T : class where TSearch : BaseSearchObject
	{
		Task<PagedResult<T>> GetAllAsync(TSearch search);
		Task<T> GetByIdAsync(int id);
	}
}

