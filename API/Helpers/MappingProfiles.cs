using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Requests.Patch;
using Domain.Requests.Upsert;

namespace API.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductDto>();
			CreateMap<ProductUpsertObject, Product>();
			CreateMap<ProductPatchObject, Product>();
		}
	}
}