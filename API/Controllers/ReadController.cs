
using API.Helpers;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Requests.Search;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReadController<T, TSearch>(IReadRepository<T, TSearch> service) : ControllerBase
		where T : class
		where TSearch : BaseSearchObject
	{
		private readonly IReadRepository<T, TSearch> _service = service;

		[Cached(120)]
		[HttpGet]
		public async Task<ActionResult<PagedResult<T>>> Get([FromQuery] TSearch search)
		{
			return Ok(await _service.GetAllAsync(search));
		}

		[Cached(120)]
		[HttpGet("{id}")]
		public async Task<ActionResult<T>> GetById(int id)
		{
			return Ok(await _service.GetByIdAsync(id));
		}
	}
}
