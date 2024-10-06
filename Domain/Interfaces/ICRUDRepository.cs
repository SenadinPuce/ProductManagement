using Domain.Requests.Search;

namespace Domain.Interfaces
{
	public interface ICRUDRepository<T, TSearch, TInsert, TUpdate, TPatch>
	   : IReadRepository<T, TSearch>
	   where T : class
	   where TSearch : BaseSearchObject
	   where TInsert : class
	   where TUpdate : class
	   where TPatch : class
	{
		Task<T> InsertAsync(TInsert insert);
		Task<T?> UpdateAsync(int id, TUpdate update);
		Task<T?> PatchAsync(int id, TPatch patch);
		Task<bool> DeleteAsync(int id);
	}
}

