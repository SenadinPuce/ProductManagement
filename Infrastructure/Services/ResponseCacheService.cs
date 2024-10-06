﻿using Domain.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
	public class ResponseCacheService(IConnectionMultiplexer redis) : IResponseCacheService
	{
		private readonly IDatabase _database = redis.GetDatabase();

		public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
		{
			if (response == null) return;

			JsonSerializerOptions jsonSerializerOptions = new()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			JsonSerializerOptions options = jsonSerializerOptions;

			var serialisedResponse = JsonSerializer.Serialize(response, options);

			await _database.StringSetAsync(cacheKey, serialisedResponse, timeToLive);
		}

		public async Task<string?> GetCachedResponse(string cacheKey)
		{
			var cachedResponse = await _database.StringGetAsync(cacheKey);

			if (cachedResponse.IsNullOrEmpty) return null;

			return cachedResponse;
		}
	}
}
