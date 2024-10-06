using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Requests.Patch;
using Domain.Requests.Search;
using Domain.Requests.Upsert;


namespace API.Controllers
{
	public class ProductsController(IProductRepository service)
	: CRUDController<ProductDto, Product, ProductSearchObject, ProductUpsertObject, ProductUpsertObject, ProductPatchObject>(service)
	{
	}
}