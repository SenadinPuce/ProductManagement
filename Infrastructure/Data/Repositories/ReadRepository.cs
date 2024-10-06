using AutoMapper;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Requests.Search;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{

	public class ReadRepository<T, TDb, TSearch>(ProductManagementContext context, IMapper mapper) : IReadRepository<T, TSearch>
	   where T : class
	   where TDb : class
	   where TSearch : BaseSearchObject
	{
		private readonly ProductManagementContext _context = context;
		private readonly IMapper _mapper = mapper;

		public virtual async Task<PagedResult<T>> GetAllAsync(TSearch search)
		{
			var query = _context.Set<TDb>().AsQueryable();

			PagedResult<T> result = new()
			{
				Count = await query.CountAsync()
			};

			query = AddFilter(query, search);

			query = AddInclude(query, search);

			if (search?.PageIndex.HasValue == true && search?.PageSize.HasValue == true)
			{
				query = query.Skip((search.PageIndex.Value - 1) * search.PageSize.Value)
					.Take(search.PageSize.Value);
			}

			var list = await query.ToListAsync();

			result.Items = _mapper.Map<IEnumerable<T>>(list);

			return result;
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			var entity = await _context.Set<TDb>().FindAsync(id);

			return _mapper.Map<T>(entity);
		}

		public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch search)
		{
			return query;
		}

		public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch search)
		{
			return query;
		}
	}

}

