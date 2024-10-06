using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Requests.Patch;
using Domain.Requests.Search;
using Domain.Requests.Upsert;


namespace Infrastructure.Data.Repositories
{
	public class ProductRepository(ProductManagementContext context, IMapper mapper)
		: CRUDRepository<ProductDto, Product, ProductSearchObject, ProductUpsertObject, ProductUpsertObject, ProductPatchObject>(context, mapper), IProductRepository
	{
		public override IQueryable<Product> AddFilter(IQueryable<Product> query, ProductSearchObject search)
		{
			if (!string.IsNullOrEmpty(search.FullTextSearch))
			{
				query = query.Where(x => x.Name.Contains(search.FullTextSearch));
			}

			return query;
		}
	}
}