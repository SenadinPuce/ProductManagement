using AutoMapper;
using Domain.Interfaces;
using Domain.Requests.Search;

namespace Infrastructure.Data.Repositories
{
	public class CRUDRepository<T, TDb, TSearch, TInsert, TUpdate, TPatch>(ProductManagementContext context, IMapper mapper)
		: ReadRepository<T, TDb, TSearch>(context, mapper),
		ICRUDRepository<T, TSearch, TInsert, TUpdate, TPatch>

		where T : class
		where TDb : class
		where TSearch : BaseSearchObject
		where TInsert : class
		where TUpdate : class
		where TPatch : class
	{
		private readonly ProductManagementContext _context = context;
		private readonly IMapper _mapper = mapper;

		public virtual async Task<T> InsertAsync(TInsert insert)
		{
			var set = _context.Set<TDb>();

			TDb entity = _mapper.Map<TDb>(insert);

			set.Add(entity);

			await _context.SaveChangesAsync();

			return _mapper.Map<T>(entity);
		}

		public virtual async Task<T?> PatchAsync(int id, TPatch patch)
		{
			var set = _context.Set<TDb>();
			var entity = await set.FindAsync(id);

			if (entity == null) return null;

			_mapper.Map(patch, entity);
			await _context.SaveChangesAsync();

			return _mapper.Map<T>(entity);
		}

		public virtual async Task<T?> UpdateAsync(int id, TUpdate update)
		{
			var set = _context.Set<TDb>();

			var entity = await set.FindAsync(id);

			if (entity == null) return null;

			_mapper.Map(update, entity);

			await _context.SaveChangesAsync();

			return _mapper.Map<T>(entity);
		}

		public virtual async Task<bool> DeleteAsync(int id)
		{
			var set = _context.Set<TDb>();

			var entity = await set.FindAsync(id);

			if (entity == null) return false;

			set.Remove(entity);

			await _context.SaveChangesAsync();

			return true;
		}
	}
}