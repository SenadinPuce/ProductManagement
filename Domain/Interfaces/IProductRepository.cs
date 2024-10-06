using Domain.DTOs;
using Domain.Requests.Patch;
using Domain.Requests.Search;
using Domain.Requests.Upsert;

namespace Domain.Interfaces
{
	public interface IProductRepository
		: ICRUDRepository<ProductDto, ProductSearchObject, ProductUpsertObject, ProductUpsertObject, ProductPatchObject>
	{

	}
}