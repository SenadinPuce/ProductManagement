
using API.Helpers;
using Domain.Interfaces;
using Domain.Requests.Search;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class CRUDController<T, TDb, TSearch, TInsert, TUpdate, TPatch>(ICRUDRepository<T, TSearch, TInsert, TUpdate, TPatch> service)
		: ReadController<T, TSearch>(service)
		where T : class
		where TDb : class
		where TInsert : class
		where TUpdate : class
		where TPatch : class
		where TSearch : BaseSearchObject
	{
		private readonly ICRUDRepository<T, TSearch, TInsert, TUpdate, TPatch> _service = service;

		[HttpPost]
		public async Task<ActionResult<T>> Insert(TInsert insert)
		{
			return Ok(await _service.InsertAsync(insert));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<T>> Update(int id, TUpdate update)
		{
			var entity = await _service.UpdateAsync(id, update);

			if (entity == null) return NotFound();

			return Ok(entity);
		}

		[HttpPatch("{id}")]
		public async Task<ActionResult<T>> Patch(int id, TPatch patch)
		{
			var entity = await _service.PatchAsync(id, patch);

			if (entity == null) return NotFound();

			return Ok(entity);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var success = await _service.DeleteAsync(id);

			if (!success) return NotFound();

			return NoContent();
		}
	}

}